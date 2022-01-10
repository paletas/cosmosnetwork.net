using Cosmos.SDK.Protos.Tx;
using Cosmos.SDK.Protos.Tx.Signing;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Terra.NET.API.Serialization.Json.Requests;
using Terra.NET.API.Serialization.Json.Responses;
using Terra.NET.API.Serialization.Protos.Mappers;

namespace Terra.NET.API.Impl
{
    internal class TransactionsApi : BaseApiSection, ITransactionsApi
    {
        public TransactionsApi(TerraApiOptions options, HttpClient httpClient, ILogger<TransactionsApi> logger) : base(options, httpClient, logger)
        {
        }

        public async Task<Transaction> CreateTransaction(IEnumerable<Message> messages, SignerOptions[] signers, CreateTransactionOptions transactionOptions, CancellationToken cancellationToken = default)
        {
            var fees = transactionOptions.Fees ?? await this.EstimateFee(messages, signers, new EstimateFeesOptions(Memo: transactionOptions.Memo, FeeDenoms: transactionOptions.FeesDenoms), cancellationToken).ConfigureAwait(false);

            return new StandardTransaction(messages.ToArray(), transactionOptions.Memo, transactionOptions.TimeoutHeight, fees, signers);
        }

        public async Task<Fee> EstimateFee(IEnumerable<Message> messages, SignerOptions[] signers, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            var gasPrices = estimateOptions?.GasPrices ?? base.Options.GasPrices ?? throw new InvalidOperationException($"Gas prices are required");
            var feeDenoms = estimateOptions?.FeeDenoms ?? base.Options.DefaultDenoms ?? throw new InvalidOperationException("Default denoms are required");
            var gasAdjustment = estimateOptions?.GasAdjustment ?? base.Options.GasAdjustment;
            var gas = estimateOptions?.Gas ?? default;

            if (gas == default)
            {
                var (errorCode, simulationResult) = await SimulateTransaction(messages, signers, estimateOptions, cancellationToken);
                if (errorCode != null) throw new InvalidOperationException();
                if (simulationResult == null || simulationResult.GasUsage == null) throw new InvalidOperationException();
                gas = simulationResult.GasUsage.GasUsed;
            }

            var transaction = CreateTransactionInternal(messages, signers, estimateOptions?.Memo, estimateOptions?.TimeoutHeight);

            using MemoryStream memoryStream = new MemoryStream();
            transaction.WriteTo(new Google.Protobuf.CodedOutputStream(memoryStream));
            var computeTaxRequest = new TransactionRequest(Convert.ToBase64String(memoryStream.GetBuffer()));
            var computeTaxResponse = await this.Post<TransactionRequest, ComputeTaxResponse>($"/terra/tx/v1beta1/compute_tax", computeTaxRequest, cancellationToken).ConfigureAwait(false);

            if (computeTaxResponse == null) throw new InvalidOperationException("Couldn't estimate fees");

            return new Fee(gas, computeTaxResponse.TaxAmount.Select(ta => ta.ToModel()).ToArray());
        }

        public async Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(IEnumerable<Message> messages, SignerOptions[] signers, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default)
        {
            var gasAdjustment = simulationOptions?.GasAdjustment ?? base.Options.GasAdjustment;
            var transaction = CreateTransactionInternal(messages, signers, simulationOptions?.Memo, simulationOptions?.TimeoutHeight);

            using var memoryStream = new MemoryStream();
            using (var outputStream = new Google.Protobuf.CodedOutputStream(memoryStream, true))
            {
                transaction.WriteTo(outputStream);
            }

            var simulationRequest = new TransactionRequest(Convert.ToBase64String(memoryStream.GetBuffer()));
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

        private Tx CreateTransactionInternal(IEnumerable<Message> messages, SignerOptions[] signers, string? memo = null, ulong? timeoutHeight = null)
        {
            var transaction = new Tx
            {
                Body = new TxBody()
                {
                    Memo = memo ?? string.Empty,
                    TimeoutHeight = timeoutHeight ?? default
                },
                AuthInfo = new AuthInfo
                {
                    Fee = new Cosmos.SDK.Protos.Tx.Fee()
                }
            };

            transaction.Body.Messages.AddRange(messages.Select(msg => msg.ToJson().PackAny(base.JsonSerializerOptions)));

            foreach (var signer in signers)
            {
                var signerInfo = new SignerInfo
                {
                    PublicKey = signer.PublicKey?.PackAny(),
                    Sequence = signer.Sequence,
                    ModeInfo = new ModeInfo
                    {
                        Single = new ModeInfo.Types.Single
                        {
                            Mode = SignMode.Direct
                        }
                    }
                };

                transaction.Signatures.Add(Google.Protobuf.ByteString.Empty);
                transaction.AuthInfo.SignerInfos.Add(signerInfo);
            }

            return transaction;
        }
    }
}
