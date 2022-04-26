using Cosmos.SDK.Protos.Tx;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Terra.NET.API.Serialization.Json.Requests;
using Terra.NET.API.Serialization.Json.Responses;
using Terra.NET.Exceptions;
using Terra.NET.Transactions;

namespace Terra.NET.API.Impl
{
    internal class TransactionsApi : BaseApiSection, ITransactionsApi
    {
        private readonly TransactionsBuilder _transactionBuilder;
        private readonly IBlockchainApi _blockchainApi;
        private readonly IBlocksApi _blocksApi;

        public TransactionsApi(TerraApiOptions options, HttpClient httpClient, ILogger<TransactionsApi> logger, TransactionsBuilder transactionBuilder, IBlockchainApi blockchainApi, IBlocksApi blocksApi) : base(options, httpClient, logger)
        {
            this._transactionBuilder = transactionBuilder;
            this._blockchainApi = blockchainApi;
            this._blocksApi = blocksApi;
        }

        public async Task<Fee> EstimateFee(IEnumerable<Message> messages, SignerOptions[] signers, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            var gasAdjustment = estimateOptions?.GasAdjustment ?? base.Options.GasAdjustment;
            var gasPrices = estimateOptions?.GasPrices ?? base.Options.GasPrices;
            if (gasPrices == null)
            {
                var updatedGasPrices = await this._blockchainApi.GetGasPrices(cancellationToken).ConfigureAwait(false);
                gasPrices = base.Options.GasPrices = updatedGasPrices.Select(gas => new CoinDecimal(gas.Key, gas.Value, true)).ToArray();

                if (gasPrices.Length == 0) throw new InvalidOperationException("Gas prices unknown");
            }

            var feeDenoms = estimateOptions?.FeesDenoms ?? base.Options.DefaultDenoms ?? throw new InvalidOperationException("Default denoms are required");
            var gas = estimateOptions?.Gas ?? default;

            if (gas == default)
            {
                var (errorCode, simulationResult) = await SimulateTransaction(messages, signers, estimateOptions, cancellationToken);
                if (errorCode.HasValue) throw new EstimateFeeException(errorCode.Value, "Unable to estimate fee");
                if (simulationResult == null || simulationResult.GasUsage == null) throw new InvalidOperationException();
                gas = simulationResult.GasUsage.GasUsed;
                gas = (ulong)Math.Ceiling(gas * gasAdjustment);
            }

            return new Fee(gas, gasPrices.Where(gp => feeDenoms.Contains(gp.Denom)).Select(gp => new NativeCoin(gp.Denom, (ulong)Math.Ceiling(gp.Amount * gas))).ToArray());
        }

        public async Task<IEnumerable<Coin>> ComputeTax(IEnumerable<Message> messages, SignerOptions[] signers, CancellationToken cancellationToken = default)
        {
            var transaction = this._transactionBuilder.CreateTransaction(messages);
            this._transactionBuilder.AddEmptySignatures(transaction, signers);

            var simulationRequest = new TransactionRequest(SerializeTransaction(transaction));
            var simulationResponse = await Post<TransactionRequest, ComputeTaxResponse, ErrorResponse>($"/terra/tx/v1beta1/compute_tax", simulationRequest, cancellationToken).ConfigureAwait(false);

            if (simulationResponse.ResponseOK == null) throw new InvalidOperationException("Couldn't compute tax for transaction");

            if (simulationResponse.ResponseOK != null)
            {
                return simulationResponse.ResponseOK.TaxAmount.Select(denomAmount => denomAmount.ToModel()).ToArray();
            }
            else throw new InvalidOperationException();
        }

        public async Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(IEnumerable<Message> messages, SignerOptions[] signers, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default)
        {
            var transaction = this._transactionBuilder.CreateTransaction(messages, simulationOptions?.Memo, simulationOptions?.TimeoutHeight);
            this._transactionBuilder.AddEmptySignatures(transaction, signers);

            var simulationRequest = new TransactionRequest(SerializeTransaction(transaction));
            TransactionSimulationResponse? simulationResponse;
            ErrorResponse? simulationErrorResponse;
            try
            {
                (simulationResponse, simulationErrorResponse) = await Post<TransactionRequest, TransactionSimulationResponse, ErrorResponse>($"/cosmos/tx/v1beta1/simulate", simulationRequest, cancellationToken).ConfigureAwait(false);
            }
            catch (InvalidResponseException ex)
            {
                uint errorCode;
                if (ex.RawResponse.StartsWith("error code: "))
                {
                    var prefixLength = "error code: ".Length;
                    uint.TryParse(ex.RawResponse.Substring(prefixLength, ex.RawResponse.Length - prefixLength), out errorCode);
                }
                else
                {
                    errorCode = uint.MaxValue;
                }

                return (errorCode, null);
            }

            if (simulationResponse == null && simulationErrorResponse == null) throw new InvalidOperationException("Couldn't simulate transaction");

            if (simulationResponse != null)
            {
                var simulationGasUsage = new TransactionGasUsage(simulationResponse.GasInfo.GasWanted, simulationResponse.GasInfo.GasUsed);
                var simulationResult = new TransactionSimulationResult(
                    simulationResponse.Result.Data,
                    simulationResponse.Result.Log,
                    simulationResponse.Result.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray()
                );

                return (null, new TransactionSimulation(simulationGasUsage, simulationResult));
            }
            else if (simulationErrorResponse != null)
            {
                return (simulationErrorResponse.Code, null);
            }
            else throw new InvalidOperationException();
        }

        public async Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            var simulationRequest = new TransactionRequest(Convert.ToBase64String(transaction.Payload));
            var simulationResponse = await Post<TransactionRequest, TransactionSimulationResponse, ErrorResponse>($"/cosmos/tx/v1beta1/simulate", simulationRequest, cancellationToken).ConfigureAwait(false);

            if (simulationResponse.ResponseOK == null && simulationResponse.ResponseError == null) throw new InvalidOperationException("Couldn't simulate transaction");

            if (simulationResponse.ResponseOK != null)
            {
                var simulationGasUsage = new TransactionGasUsage(simulationResponse.ResponseOK.GasInfo.GasWanted, simulationResponse.ResponseOK.GasInfo.GasUsed);
                var simulationResult = new TransactionSimulationResult(
                    simulationResponse.ResponseOK.Result.Data,
                    simulationResponse.ResponseOK.Result.Log,
                    simulationResponse.ResponseOK.Result.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray()
                );

                return (null, new TransactionSimulation(simulationGasUsage, simulationResult));
            }
            else if (simulationResponse.ResponseError != null)
            {
                return (simulationResponse.ResponseError.Code, null);
            }
            else throw new InvalidOperationException();
        }

        public async Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionBlock(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            var broadcastRequest = new TransactionRequest(Convert.ToBase64String(transaction.Payload), "BROADCAST_MODE_BLOCK");
            var broadcastResponse = await Post<TransactionRequest, TransactionBroadcastResponse, ErrorResponse>($"/cosmos/tx/v1beta1/txs", broadcastRequest, cancellationToken).ConfigureAwait(false);

            if (broadcastResponse.ResponseOK == null && broadcastResponse.ResponseError == null) throw new InvalidOperationException("Couldn't broadcast transaction");

            if (broadcastResponse.ResponseOK != null)
            {
                var result = broadcastResponse.ResponseOK.Result;

                var gasUsage = new TransactionGasUsage(result.GasWanted, result.GasUsed);
                var broadcastResult = new TransactionResult(
                    result.Data,
                    result.Info,
                    result.Logs.Select(log => new TransactionLog(
                        log.MessageIndex,
                        log.Log.ToString(),
                        log.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray())
                    ).ToArray(),
                    result.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray()
                );

                return (null, new TransactionBroadcast(transaction, gasUsage, broadcastResult));
            }
            else if (broadcastResponse.ResponseError != null)
            {
                return (broadcastResponse.ResponseError.Code, null);
            }
            else throw new InvalidOperationException();
        }

        public async Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionAsyncAndWait(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            var broadcastRequest = new TransactionRequest(Convert.ToBase64String(transaction.Payload), "BROADCAST_MODE_ASYNC");
            var broadcastResponse = await Post<TransactionRequest, TransactionBroadcastResponse, ErrorResponse>($"/cosmos/tx/v1beta1/txs", broadcastRequest, cancellationToken).ConfigureAwait(false);

            if (broadcastResponse.ResponseOK == null && broadcastResponse.ResponseError == null) throw new InvalidOperationException("Couldn't broadcast transaction");

            if (broadcastResponse.ResponseOK != null)
            {
                var result = broadcastResponse.ResponseOK.Result;
                var transactionHash = result.TransactionHash;

                var tx = await GetTransaction(transactionHash, cancellationToken);
                while (tx == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                    tx = await GetTransaction(transactionHash, cancellationToken);
                }

                var gasUsage = new TransactionGasUsage(tx.GasWanted, tx.GasUsed);
                var txResult = new TransactionResult(tx.RawLog, string.Empty, tx.Logs, tx.Logs.SelectMany(l => l.Events).ToArray());
                return (null, new TransactionBroadcast(transaction, gasUsage, txResult));
            }
            else if (broadcastResponse.ResponseError != null)
            {
                return (broadcastResponse.ResponseError.Code, null);
            }
            else throw new InvalidOperationException();
        }

        public async Task<(uint? ErrorCode, string? TransactionHash)> BroadcastTransactionAsync(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            var broadcastRequest = new TransactionRequest(Convert.ToBase64String(transaction.Payload), "BROADCAST_MODE_ASYNC");
            var broadcastResponse = await Post<TransactionRequest, TransactionBroadcastResponse, ErrorResponse>($"/cosmos/tx/v1beta1/txs", broadcastRequest, cancellationToken).ConfigureAwait(false);

            if (broadcastResponse.ResponseOK == null && broadcastResponse.ResponseError == null) throw new InvalidOperationException("Couldn't broadcast transaction");

            if (broadcastResponse.ResponseOK != null)
            {
                var result = broadcastResponse.ResponseOK.Result;
                var transactionHash = result.TransactionHash;

                return (null, transactionHash);
            }
            else if (broadcastResponse.ResponseError != null)
            {
                return (broadcastResponse.ResponseError.Code, null);
            }
            else throw new InvalidOperationException();
        }

        public async Task<BlockTransaction?> GetTransaction(string transactionHash, CancellationToken cancellationToken = default)
        {
            var endpoint = $"/cosmos/tx/v1beta1/txs/{transactionHash}";

            var transactionResponse = await Get<Serialization.Json.BlockTransaction>(endpoint, cancellationToken);

            return transactionResponse?.ToModel();
        }

        public async IAsyncEnumerable<BlockTransaction> GetTransactions(ulong height, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var block = await this._blocksApi.GetBlock(height, cancellationToken);

            if (block is null) yield break;

            foreach (var tx in block.Details.Data.Transactions)
            {
                var txHash = HashExtensions.HashToHex(tx);

                yield return await this.GetTransaction(txHash, cancellationToken) ?? throw new TerraApiException();
            }
        }

        private static string SerializeTransaction(Tx transaction)
        {
            using var memoryStream = new MemoryStream();
            using (var outputStream = new Google.Protobuf.CodedOutputStream(memoryStream, true))
            {
                transaction.WriteTo(outputStream);
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}
