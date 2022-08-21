using CosmosNetwork.Exceptions;
using CosmosNetwork.Serialization.Json.Requests;
using CosmosNetwork.Serialization.Json.Responses;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace CosmosNetwork.API.Impl
{
    internal class TransactionsApi : BaseApiSection, ITransactionsApi
    {
        private readonly IBlockchainApi _blockchainApi;
        private readonly IBlocksApi _blocksApi;

        public TransactionsApi(CosmosApiOptions options, HttpClient httpClient, ILogger<TransactionsApi> logger, IBlockchainApi blockchainApi, IBlocksApi blocksApi) : base(options, httpClient, logger)
        {
            _blockchainApi = blockchainApi;
            _blocksApi = blocksApi;
        }

        public async Task<Fee> EstimateFee(IEnumerable<Message> messages, SignerOptions[] signers, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            decimal gasAdjustment = estimateOptions?.GasAdjustment ?? Options.GasAdjustment;
            CoinDecimal[]? gasPrices = estimateOptions?.GasPrices ?? Options.GasPrices;
            if (gasPrices == null)
            {
                IReadOnlyDictionary<string, decimal> updatedGasPrices = await _blockchainApi.GetGasPrices(cancellationToken).ConfigureAwait(false);
                gasPrices = Options.GasPrices = updatedGasPrices.Select(gas => new CoinDecimal(gas.Key, gas.Value, true)).ToArray();

                if (gasPrices.Length == 0)
                {
                    throw new InvalidOperationException("Gas prices unknown");
                }
            }

            string[] feeDenoms = estimateOptions?.FeesDenoms ?? Options.DefaultDenoms ?? throw new InvalidOperationException("Default denoms are required");
            ulong gas = estimateOptions?.Gas ?? default;

            if (gas == default)
            {
                (uint? errorCode, TransactionSimulation simulationResult) = await SimulateTransaction(messages, signers, estimateOptions, cancellationToken);
                if (errorCode.HasValue)
                {
                    throw new EstimateFeeException(errorCode.Value, "Unable to estimate fee");
                }

                if (simulationResult == null || simulationResult.GasUsage == null)
                {
                    throw new InvalidOperationException();
                }

                gas = simulationResult.GasUsage.GasUsed;
                gas = (ulong)Math.Ceiling(gas * gasAdjustment);
            }

            return new Fee(gas, gasPrices.Where(gp => feeDenoms.Contains(gp.Denom)).Select(gp => new NativeCoin(gp.Denom, (ulong)Math.Ceiling(gp.Amount * gas))).ToArray());
        }

        public Task<IEnumerable<Coin>> ComputeTax(IEnumerable<Message> messages, SignerOptions[] signers, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();

            //var simulationRequest = new TransactionRequest(SerializeTransaction(transaction));
            //var simulationResponse = await Post<TransactionRequest, ComputeTaxResponse, ErrorResponse>($"/terra/tx/v1beta1/compute_tax", simulationRequest, cancellationToken).ConfigureAwait(false);

            //if (simulationResponse.ResponseOK == null) throw new InvalidOperationException("Couldn't compute tax for transaction");

            //if (simulationResponse.ResponseOK != null)
            //{
            //    return simulationResponse.ResponseOK.TaxAmount.Select(denomAmount => denomAmount.ToModel()).ToArray();
            //}
            //else throw new InvalidOperationException();
        }

        public Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(IEnumerable<Message> messages, SignerOptions[] signers, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();

            //var simulationRequest = new TransactionRequest(SerializeTransaction(transaction));
            //TransactionSimulationResponse? simulationResponse;
            //ErrorResponse? simulationErrorResponse;
            //try
            //{
            //    (simulationResponse, simulationErrorResponse) = await Post<TransactionRequest, TransactionSimulationResponse, ErrorResponse>($"/cosmos/tx/v1beta1/simulate", simulationRequest, cancellationToken).ConfigureAwait(false);
            //}
            //catch (InvalidResponseException ex)
            //{
            //    uint errorCode;
            //    if (ex.RawResponse.StartsWith("error code: "))
            //    {
            //        var prefixLength = "error code: ".Length;
            //        uint.TryParse(ex.RawResponse.Substring(prefixLength, ex.RawResponse.Length - prefixLength), out errorCode);
            //    }
            //    else
            //    {
            //        errorCode = uint.MaxValue;
            //    }

            //    return (errorCode, null);
            //}

            //if (simulationResponse == null && simulationErrorResponse == null) throw new InvalidOperationException("Couldn't simulate transaction");

            //if (simulationResponse != null)
            //{
            //    var simulationGasUsage = new TransactionGasUsage(simulationResponse.GasInfo.GasWanted, simulationResponse.GasInfo.GasUsed);
            //    var simulationResult = new TransactionSimulationResult(
            //        simulationResponse.Result.Data,
            //        simulationResponse.Result.Log,
            //        simulationResponse.Result.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray()
            //    );

            //    return (null, new TransactionSimulation(simulationGasUsage, simulationResult));
            //}
            //else if (simulationErrorResponse != null)
            //{
            //    return (simulationErrorResponse.Code, null);
            //}
            //else throw new InvalidOperationException();
        }

        public async Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            TransactionRequest simulationRequest = new(Convert.ToBase64String(transaction.Payload));
            (TransactionSimulationResponse? ResponseOK, ErrorResponse? ResponseError) = await Post<TransactionRequest, TransactionSimulationResponse, ErrorResponse>($"/cosmos/tx/v1beta1/simulate", simulationRequest, cancellationToken).ConfigureAwait(false);

            if (ResponseOK == null && ResponseError == null)
            {
                throw new InvalidOperationException("Couldn't simulate transaction");
            }

            if (ResponseOK != null)
            {
                TransactionGasUsage simulationGasUsage = new(ResponseOK.GasInfo.GasWanted, ResponseOK.GasInfo.GasUsed);
                TransactionSimulationResult simulationResult = new(
                    ResponseOK.Result.Data,
                    ResponseOK.Result.Log,
                    ResponseOK.Result.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray()
                );

                return (null, new TransactionSimulation(simulationGasUsage, simulationResult));
            }
            else
            {
                return ResponseError != null
                    ? ((uint? ErrorCode, TransactionSimulation? Result))(ResponseError.Code, null)
                    : throw new InvalidOperationException();
            }
        }

        public async Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionBlock(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            TransactionRequest broadcastRequest = new(Convert.ToBase64String(transaction.Payload), "BROADCAST_MODE_BLOCK");
            (TransactionBroadcastResponse? ResponseOK, ErrorResponse? ResponseError) = await Post<TransactionRequest, TransactionBroadcastResponse, ErrorResponse>($"/cosmos/tx/v1beta1/txs", broadcastRequest, cancellationToken).ConfigureAwait(false);

            if (ResponseOK == null && ResponseError == null)
            {
                throw new InvalidOperationException("Couldn't broadcast transaction");
            }

            if (ResponseOK != null)
            {
                Serialization.Json.TransactionResponse result = ResponseOK.Result;

                TransactionGasUsage gasUsage = new(result.GasWanted, result.GasUsed);
                TransactionResult broadcastResult = new(
                    result.Data,
                    result.Info,
                    result.Logs.Select(log => new TransactionLog(
                        log.MessageIndex,
                        log.Log?.ToString(),
                        log.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray())
                    ).ToArray(),
                    result.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray()
                );

                return (null, new TransactionBroadcast(transaction, gasUsage, broadcastResult));
            }
            else
            {
                return ResponseError != null
                    ? ((uint? ErrorCode, TransactionBroadcast? Result))(ResponseError.Code, null)
                    : throw new InvalidOperationException();
            }
        }

        public async Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionAsyncAndWait(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            TransactionRequest broadcastRequest = new(Convert.ToBase64String(transaction.Payload), "BROADCAST_MODE_ASYNC");
            (TransactionBroadcastResponse? ResponseOK, ErrorResponse? ResponseError) = await Post<TransactionRequest, TransactionBroadcastResponse, ErrorResponse>($"/cosmos/tx/v1beta1/txs", broadcastRequest, cancellationToken).ConfigureAwait(false);

            if (ResponseOK == null && ResponseError == null)
            {
                throw new InvalidOperationException("Couldn't broadcast transaction");
            }

            if (ResponseOK != null)
            {
                Serialization.Json.TransactionResponse result = ResponseOK.Result;
                string transactionHash = result.TransactionHash;

                BlockTransaction? tx = await GetTransaction(transactionHash, cancellationToken);
                while (tx == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                    tx = await GetTransaction(transactionHash, cancellationToken);
                }

                TransactionGasUsage gasUsage = new(tx.GasWanted, tx.GasUsed);
                TransactionResult txResult = new(tx.RawLog, string.Empty, tx.Logs, tx.Logs.SelectMany(l => l.Events).ToArray());
                return (null, new TransactionBroadcast(transaction, gasUsage, txResult));
            }
            else
            {
                return ResponseError != null
                    ? ((uint? ErrorCode, TransactionBroadcast? Result))(ResponseError.Code, null)
                    : throw new InvalidOperationException();
            }
        }

        public async Task<(uint? ErrorCode, string? TransactionHash)> BroadcastTransactionAsync(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            TransactionRequest broadcastRequest = new(Convert.ToBase64String(transaction.Payload), "BROADCAST_MODE_ASYNC");
            (TransactionBroadcastResponse? ResponseOK, ErrorResponse? ResponseError) = await Post<TransactionRequest, TransactionBroadcastResponse, ErrorResponse>($"/cosmos/tx/v1beta1/txs", broadcastRequest, cancellationToken).ConfigureAwait(false);

            if (ResponseOK == null && ResponseError == null)
            {
                throw new InvalidOperationException("Couldn't broadcast transaction");
            }

            if (ResponseOK != null)
            {
                Serialization.Json.TransactionResponse result = ResponseOK.Result;
                string transactionHash = result.TransactionHash;

                return (null, transactionHash);
            }
            else
            {
                return ResponseError != null
                    ? ((uint? ErrorCode, string? TransactionHash))(ResponseError.Code, null)
                    : throw new InvalidOperationException();
            }
        }

        public async Task<BlockTransaction?> GetTransaction(string transactionHash, CancellationToken cancellationToken = default)
        {
            string endpoint = $"/cosmos/tx/v1beta1/txs/{transactionHash}";

            Serialization.Json.BlockTransaction? transactionResponse = await Get<Serialization.Json.BlockTransaction>(endpoint, cancellationToken);

            return transactionResponse?.ToModel();
        }

        public async IAsyncEnumerable<BlockTransaction> GetTransactions(ulong height, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            Block? block = await _blocksApi.GetBlock(height, cancellationToken);

            if (block is null)
            {
                yield break;
            }

            foreach (string tx in block.Details.Data.Transactions)
            {
                string txHash = HashExtensions.HashToHex(tx);

                yield return await GetTransaction(txHash, cancellationToken) ?? throw new CosmosException();
            }
        }

        private static string SerializeTransaction(Serialization.Transaction transaction)
        {
            return Convert.ToBase64String(transaction.ToByteArray());
        }
    }
}
