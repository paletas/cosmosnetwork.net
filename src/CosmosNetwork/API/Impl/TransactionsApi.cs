using CosmosNetwork.Exceptions;
using CosmosNetwork.Serialization.Json.Requests;
using CosmosNetwork.Serialization.Json.Responses;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace CosmosNetwork.API.Impl
{
    internal class TransactionsApi : BaseApiSection, ITransactionsApi
    {
        private readonly IBlocksApi _blocksApi;

        public TransactionsApi(CosmosApiOptions options, HttpClient httpClient, ILogger<TransactionsApi> logger, IBlocksApi blocksApi) : base(options, httpClient, logger)
        {
            _blocksApi = blocksApi;
        }

        public async IAsyncEnumerable<BlockTransaction> GetTransactions(ulong height, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            Block? block = await _blocksApi.GetBlock(height, cancellationToken);

            if (block is null) throw new ArgumentException("block not found at height", nameof(height));

            foreach (string tx in block.Details.Data.Transactions)
            {
                string txHash = HashExtensions.HashToHex(tx);

                yield return await GetTransaction(txHash, cancellationToken) ?? throw new CosmosException();
            }
        }

        public async Task<BlockTransaction?> GetTransaction(string transactionHash, CancellationToken cancellationToken = default)
        {
            string endpoint = $"/cosmos/tx/v1beta1/txs/{transactionHash}";

            Serialization.Json.BlockTransaction? transactionResponse = await Get<Serialization.Json.BlockTransaction>(endpoint, cancellationToken);

            return transactionResponse?.ToModel();
        }

        public async Task<Fee> EstimateFee(ITransactionPayload transaction, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            decimal gasAdjustment = estimateOptions?.GasAdjustment ?? Options.GasAdjustment;
            CoinDecimal[]? gasPrices = estimateOptions?.GasPrices ?? Options.GasPrices;

            if (gasPrices is null) throw new InvalidOperationException("Gas prices unknown");

            string[] feeDenoms = estimateOptions?.FeesDenoms ?? Options.DefaultDenoms ?? throw new InvalidOperationException("Default denoms are required");
            ulong gas = estimateOptions?.Gas ?? default;

            if (gas == default)
            {
                (uint? errorCode, TransactionSimulationResults? simulationResult) = await SimulateTransaction(transaction, cancellationToken);
                if (errorCode.HasValue)
                {
                    throw new EstimateFeeException(errorCode.Value, "Unable to estimate fee");
                }

                if (simulationResult?.GasUsage is null)
                {
                    throw new InvalidOperationException();
                }

                gas = simulationResult.GasUsage.GasUsed;
                gas = (ulong)Math.Ceiling(gas * gasAdjustment);
            }

            return new Fee(gas, gasPrices.Where(gp => feeDenoms.Contains(gp.Denom)).Select(gp => new NativeCoin(gp.Denom, (ulong)Math.Ceiling(gp.Amount * gas))).ToArray());
        }

        public async Task<(uint? ErrorCode, TransactionSimulationResults? Result)> SimulateTransaction(ITransactionPayload transaction, CancellationToken cancellationToken = default)
        {
            TransactionRequest simulationRequest = new(transaction.GetBase64());
            (TransactionSimulationResponse? ResponseOK, ErrorResponse? ResponseError) = await Post<TransactionRequest, TransactionSimulationResponse, ErrorResponse>($"/cosmos/tx/v1beta1/simulate", simulationRequest, cancellationToken).ConfigureAwait(false);

            if (ResponseOK == null && ResponseError == null)
            {
                throw new InvalidOperationException("Couldn't simulate transaction");
            }

            if (ResponseOK != null)
            {
                GasUsage simulationGasUsage = new(ResponseOK.GasInfo.GasWanted, ResponseOK.GasInfo.GasUsed);
                SimulationResult simulationResult = new(
                    ResponseOK.Result.Data,
                    ResponseOK.Result.Log,
                    ResponseOK.Result.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray()
                );

                return (null, new TransactionSimulationResults(simulationGasUsage, simulationResult));
            }
            else
            {
                return ResponseError != null
                    ? ((uint? ErrorCode, TransactionSimulationResults? Result))(ResponseError.Code, null)
                    : throw new InvalidOperationException();
            }
        }

        public async Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransaction(ITransactionPayload transaction, CancellationToken cancellationToken = default)
        {
            TransactionRequest broadcastRequest = new(transaction.GetBase64(), "BROADCAST_MODE_BLOCK");
            (TransactionBroadcastResponse? ResponseOK, ErrorResponse? ResponseError) = await Post<TransactionRequest, TransactionBroadcastResponse, ErrorResponse>($"/cosmos/tx/v1beta1/txs", broadcastRequest, cancellationToken).ConfigureAwait(false);

            if (ResponseOK == null && ResponseError == null)
            {
                throw new InvalidOperationException("Couldn't broadcast transaction");
            }

            if (ResponseOK != null)
            {
                Serialization.Json.TransactionResponse result = ResponseOK.Result;

                GasUsage gasUsage = new(result.GasWanted, result.GasUsed);
                ExecutionResult broadcastResult = new(
                    result.Data,
                    result.Info,
                    result.Logs.Select(log => new TransactionLog(
                        log.MessageIndex,
                        log.Log?.ToString(),
                        log.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray())
                    ).ToArray(),
                    result.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray()
                );

                return (null, new TransactionBroadcastResults(gasUsage, broadcastResult));
            }
            else
            {
                return ResponseError != null
                    ? ((uint? ErrorCode, TransactionBroadcastResults? Result))(ResponseError.Code, null)
                    : throw new InvalidOperationException();
            }
        }

        public async Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionAsync(ITransactionPayload transaction, CancellationToken cancellationToken = default)
        {
            TransactionRequest broadcastRequest = new(transaction.GetBase64(), "BROADCAST_MODE_ASYNC");
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

                GasUsage gasUsage = new(tx.GasWanted, tx.GasUsed);
                ExecutionResult txResult = new(tx.RawLog, string.Empty, tx.Logs, tx.Logs.SelectMany(l => l.Events).ToArray());
                return (null, new TransactionBroadcastResults(gasUsage, txResult));
            }
            else
            {
                return ResponseError != null
                    ? ((uint? ErrorCode, TransactionBroadcastResults? Result))(ResponseError.Code, null)
                    : throw new InvalidOperationException();
            }
        }
    }
}
