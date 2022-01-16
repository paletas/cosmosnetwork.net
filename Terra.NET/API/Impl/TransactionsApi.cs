using Cosmos.SDK.Protos.Tx;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Terra.NET.API.Internal;
using Terra.NET.API.Serialization.Json.Requests;
using Terra.NET.API.Serialization.Json.Responses;

namespace Terra.NET.API.Impl
{
    internal class TransactionsApi : BaseApiSection, ITransactionsApi
    {
        private readonly TransactionsBuilder _transactionBuilder;
        private readonly IBlockchainApi _blockchainApi;

        public TransactionsApi(TerraApiOptions options, HttpClient httpClient, ILogger<TransactionsApi> logger, TransactionsBuilder transactionBuilder, IBlockchainApi blockchainApi) : base(options, httpClient, logger)
        {
            this._transactionBuilder = transactionBuilder;
            this._blockchainApi = blockchainApi;
        }

        public async Task<Transaction> CreateTransaction(IEnumerable<Message> messages, SignerOptions[] signers, CreateTransactionOptions transactionOptions, CancellationToken cancellationToken = default)
        {
            var fees = transactionOptions.Fees ?? await this.EstimateFee(messages, signers, new EstimateFeesOptions(Memo: transactionOptions.Memo, FeeDenoms: transactionOptions.FeesDenoms), cancellationToken).ConfigureAwait(false);

            return new StandardTransaction(messages.ToArray(), transactionOptions.Memo, transactionOptions.TimeoutHeight, fees, signers);
        }

        public async Task<Fee> EstimateFee(IEnumerable<Message> messages, SignerOptions[] signers, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            var gasAdjustment = estimateOptions.GasAdjustment ?? base.Options.GasAdjustment;
            var gasPrices = estimateOptions?.GasPrices ?? base.Options.GasPrices;
            if (gasPrices == null)
            {
                var updatedGasPrices = await _blockchainApi.GetGasPrices(cancellationToken).ConfigureAwait(false);
                gasPrices = base.Options.GasPrices = updatedGasPrices.Select(gas => new CoinDecimal(gas.Key, gas.Value, true)).ToArray();
            }

            var feeDenoms = estimateOptions?.FeeDenoms ?? base.Options.DefaultDenoms ?? throw new InvalidOperationException("Default denoms are required");
            var gas = estimateOptions?.Gas ?? default;

            if (gas == default)
            {
                var (errorCode, simulationResult) = await SimulateTransaction(messages, signers, estimateOptions, cancellationToken);
                if (errorCode != null) throw new InvalidOperationException();
                if (simulationResult == null || simulationResult.GasUsage == null) throw new InvalidOperationException();
                gas = simulationResult.GasUsage.GasUsed;
                gas = (ulong) Math.Ceiling(gas * gasAdjustment);
            }

            return new Fee(gas, gasPrices.Where(gp => feeDenoms.Contains(gp.Denom)).Select(gp => new Coin(gp.Denom, (ulong) Math.Ceiling(gp.Amount * gas), true)).ToArray());
        }

        public async Task<IEnumerable<Coin>> ComputeTax(IEnumerable<Message> messages, SignerOptions[] signers, CancellationToken cancellationToken = default)
        {
            var transaction = _transactionBuilder.CreateTransaction(messages);
            _transactionBuilder.AddEmptySignatures(transaction, signers);

            var simulationRequest = new TransactionRequest(SerializeTransaction(transaction));
            var simulationResponse = await this.Post<TransactionRequest, ComputeTaxResponse, ErrorResponse>($"/terra/tx/v1beta1/compute_tax", simulationRequest, cancellationToken).ConfigureAwait(false);

            if (simulationResponse.ResponseOK == null) throw new InvalidOperationException("Couldn't compute tax for transaction");

            if (simulationResponse.ResponseOK != null)
            {
                return simulationResponse.ResponseOK.TaxAmount.Select(denomAmount => denomAmount.ToModel()).ToArray();
            }
            else throw new InvalidOperationException();
        }

        public async Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(IEnumerable<Message> messages, SignerOptions[] signers, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default)
        {
            var transaction = _transactionBuilder.CreateTransaction(messages, simulationOptions?.Memo, simulationOptions?.TimeoutHeight);
            _transactionBuilder.AddEmptySignatures(transaction, signers);

            var simulationRequest = new TransactionRequest(SerializeTransaction(transaction));
            var simulationResponse = await this.Post<TransactionRequest, TransactionSimulationResponse, ErrorResponse>($"/cosmos/tx/v1beta1/simulate", simulationRequest, cancellationToken).ConfigureAwait(false);

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

        public async Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            var simulationRequest = new TransactionRequest(Convert.ToBase64String(transaction.Payload));
            var simulationResponse = await this.Post<TransactionRequest, TransactionSimulationResponse, ErrorResponse>($"/cosmos/tx/v1beta1/simulate", simulationRequest, cancellationToken).ConfigureAwait(false);

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

        public async Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransaction(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            var broadcastRequest = new TransactionRequest(Convert.ToBase64String(transaction.Payload), "BROADCAST_MODE_BLOCK");
            var broadcastResponse = await this.Post<TransactionRequest, TransactionBroadcastResponse, ErrorResponse>($"/cosmos/tx/v1beta1/txs", broadcastRequest, cancellationToken).ConfigureAwait(false);

            if (broadcastResponse.ResponseOK == null && broadcastResponse.ResponseError == null) throw new InvalidOperationException("Couldn't broadcast transaction");

            if (broadcastResponse.ResponseOK != null)
            {
                var result = broadcastResponse.ResponseOK.Result;

                var gasUsage = new TransactionGasUsage(result.GasWanted, result.GasUsed);
                var broadcastResult = new TransactionBroadcastResult(
                    result.Data,
                    result.Info,
                    result.Logs.Select(log => new TransactionLog(
                        log.MessageIndex, 
                        log.Log, 
                        log.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray())
                    ).ToArray(),
                    result.Events.Select(te => new TransactionEvent(te.Type, te.Attributes.Select(tea => new TransactionEventAttribute(tea.Key, tea.Value)).ToArray())).ToArray()
                );

                return (null, new TransactionBroadcast(gasUsage, broadcastResult));
            }
            else if (broadcastResponse.ResponseError != null)
            {
                return (broadcastResponse.ResponseError.Code, null);
            }
            else throw new InvalidOperationException();
        }

        public async IAsyncEnumerable<BlockTransaction> GetAccountTransactions(TerraAddress accountAddress, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var endpoint = $"v1/txs?account={accountAddress}";

            var transactionsResponse = await this.Get<ListTransactionsResponse>(endpoint, cancellationToken);

            if (transactionsResponse == null) yield break;

            while (transactionsResponse != null && transactionsResponse.Transactions?.Length > 0)
            {
                foreach (var transaction in transactionsResponse.Transactions)
                {
                    yield return transaction.ToModel();
                }

                if (Options.ThrottlingEnumeratorsInMilliseconds.HasValue)
                    await Task.Delay(Options.ThrottlingEnumeratorsInMilliseconds.Value, cancellationToken).ConfigureAwait(false);

                if (transactionsResponse.Limit >= transactionsResponse.Transactions.Length)
                    transactionsResponse = await this.Get<ListTransactionsResponse>($"{endpoint}&offset={transactionsResponse.Transactions.Min(tx => tx.Id)}", cancellationToken);
                else break;
            }
        }

        public async IAsyncEnumerable<BlockTransaction> GetTransactions(long? fromHeight = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var blocksLatestEndpoint = $"/blocks/latest";
            var transactionsEndpoint = $"v1/txs?block={0}";
            var transactionsOffsetEndpoint = $"v1/txs?block={0}&offset={1}";

            var latestBlockResponse = await this.Get<GetLatestBlockResponse>(blocksLatestEndpoint, cancellationToken);
            if (latestBlockResponse == null) throw new InvalidOperationException();

            long latestBlockHeight = latestBlockResponse.Block.Header.Height;
            if (fromHeight != null && fromHeight > latestBlockHeight) throw new ArgumentOutOfRangeException(nameof(fromHeight));

            long currentHeight = fromHeight ?? base.Options.StartingBlockHeightForTransactionSearch;

            var transactionsResponse = await this.Get<ListTransactionsResponse>(string.Format(transactionsEndpoint, currentHeight));

            while (transactionsResponse != null && transactionsResponse.Transactions?.Length > 0)
            {
                foreach (var transaction in transactionsResponse.Transactions)
                {
                    yield return transaction.ToModel();
                }

                if (Options.ThrottlingEnumeratorsInMilliseconds.HasValue)
                    await Task.Delay(Options.ThrottlingEnumeratorsInMilliseconds.Value, cancellationToken).ConfigureAwait(false);

                if (transactionsResponse.Limit >= transactionsResponse.Transactions.Length)
                {
                    transactionsResponse = await this.Get<ListTransactionsResponse>(string.Format(transactionsOffsetEndpoint, currentHeight, transactionsResponse.Transactions.Min(tx => tx.Id)), cancellationToken);
                }
                else
                {
                    currentHeight++;
                    transactionsResponse = await this.Get<ListTransactionsResponse>(string.Format(transactionsEndpoint, currentHeight));
                }
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
