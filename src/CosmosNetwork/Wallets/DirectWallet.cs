using CosmosNetwork.API;
using CosmosNetwork.Keys;
using System.Runtime.CompilerServices;

namespace CosmosNetwork.Wallets
{
    internal class DirectWallet : IWallet
    {
        private readonly CosmosApiOptions _options;
        private readonly IWalletApi _walletApi;
        private readonly ITransactionsApi _transactionsApi;
        private readonly Key _key;

        public DirectWallet(CosmosApiOptions options, Key key, IWalletApi walletApi, ITransactionsApi transactionsApi)
        {
            _options = options;
            _walletApi = walletApi;
            _transactionsApi = transactionsApi;
            _key = key;
        }

        public PublicKey PublicKey => _key.PublicKey;

        public CosmosAddress? AccountAddress => PublicKey is null ? null : (CosmosAddress)PublicKey.Address;

        public string? AccountNumber { get; private set; }

        public ulong? Sequence { get; private set; }

        protected bool RequiresAccountUpdate { get; private set; } = true;

        public IEnumerable<IKey> GetKeys()
        {
            yield return _key;
        }

        public async Task<AccountInformation?> GetAccountInformation(CancellationToken cancellationToken = default)
        {
            if (AccountAddress == null)
            {
                throw new InvalidOperationException("Unable to determine Account Address");
            }

            AccountInformation? accountInformation = await _walletApi.GetAccountInformation(AccountAddress, cancellationToken).ConfigureAwait(false);
            if (accountInformation != null)
            {
                AccountNumber = accountInformation.AccountNumber;
                Sequence = accountInformation.AccountSequence;
            }
            return accountInformation;
        }

        public async Task UpdateAccountInformation(CancellationToken cancellationToken = default)
        {
            AccountInformation? accountInformation = await this.GetAccountInformation(cancellationToken);

            if (accountInformation is null) throw new InvalidOperationException();

            this.AccountNumber = accountInformation.AccountNumber;
            this.Sequence = accountInformation.AccountSequence;
            this.RequiresAccountUpdate = false;
        }

        public Task<AccountBalances> GetBalances(CancellationToken cancellationToken = default)
        {
            return AccountAddress == null
                ? throw new InvalidOperationException("Unable to determine Account Address")
                : _walletApi.GetAccountBalances(AccountAddress, cancellationToken);
        }

        public async Task<SerializedTransaction> CreateSignedTransaction(IEnumerable<Message> messages, CreateTransactionOptions? transactionOptions = null, CancellationToken cancellationToken = default)
        {
            if (this.RequiresAccountUpdate)
            {
                await UpdateAccountInformation(cancellationToken).ConfigureAwait(false);
            }

            if (transactionOptions?.Fees is null) throw new InvalidOperationException();

            var fees = transactionOptions?.Fees?.ToSerialization();

            Serialization.Transaction transaction = new(
                new Serialization.TransactionBody(messages.Select(msg => msg.ToSerialization()).ToArray(), transactionOptions?.TimeoutHeight, transactionOptions?.Memo),
                new Serialization.AuthInfo(new List<Serialization.SignatureDescriptor>(), fees),
                new List<byte[]>());

            byte[] serializedTx = transaction.GetBytes();
            return new SerializedTransaction(serializedTx, transactionOptions.Fees);
        }

        public async Task<(uint? ErrorCode, TransactionSimulationResults? Result)> SimulateTransaction(IEnumerable<Message> messages, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default)
        {
            var fees = simulationOptions?.Fees?.ToSerialization();
            if (fees is null) throw new InvalidOperationException();

            Serialization.Transaction transaction = new(
                new Serialization.TransactionBody(messages.Select(msg => msg.ToSerialization()).ToArray(), simulationOptions?.TimeoutHeight, simulationOptions?.Memo),
                new Serialization.AuthInfo(new List<Serialization.SignatureDescriptor>(), fees),
                new List<byte[]>());

            return await _transactionsApi.SimulateTransaction(transaction, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Fee> EstimateFee(IEnumerable<Message> messages, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            var fees = estimateOptions?.Fees?.ToSerialization();
            if (fees is null) throw new InvalidOperationException();

            Serialization.Transaction transaction = new(
                new Serialization.TransactionBody(messages.Select(msg => msg.ToSerialization()).ToArray(), estimateOptions?.TimeoutHeight, estimateOptions?.Memo),
                new Serialization.AuthInfo(new List<Serialization.SignatureDescriptor>(), fees),
                new List<byte[]>());

            return await _transactionsApi.EstimateFee(transaction, estimateOptions, cancellationToken).ConfigureAwait(false);
        }

        public Task<(uint? ErrorCode, TransactionSimulationResults? Result)> SimulateTransaction(SerializedTransaction transaction, CancellationToken cancellationToken = default)
        {
            return _transactionsApi.SimulateTransaction(transaction.ToSerialization(), cancellationToken);
        }

        public async Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionBlock(IEnumerable<Message> messages, BroadcastTransactionOptions? broadcastOptions = null, CancellationToken cancellationToken = default)
        {
            SerializedTransaction signedTransaction = await CreateSignedTransaction(messages, new CreateTransactionOptions(broadcastOptions?.Memo, broadcastOptions?.TimeoutHeight, broadcastOptions?.Fees, Gas: broadcastOptions?.Gas, GasPrices: null, broadcastOptions?.FeesDenoms), cancellationToken).ConfigureAwait(false);

            try
            {
                return await _transactionsApi.BroadcastTransaction(signedTransaction.ToSerialization(), cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                Sequence++;
            }
        }

        public Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionBlock(SerializedTransaction transaction, CancellationToken cancellationToken = default)
        {
            try
            {
                return _transactionsApi.BroadcastTransaction(transaction.ToSerialization(), cancellationToken);
            }
            finally
            {
                Sequence++;
            }
        }

        public async Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionAsyncAndWait(IEnumerable<Message> messages, BroadcastTransactionOptions? broadcastOptions = null, CancellationToken cancellationToken = default)
        {
            SerializedTransaction signedTransaction = await CreateSignedTransaction(messages, new CreateTransactionOptions(broadcastOptions?.Memo, broadcastOptions?.TimeoutHeight, broadcastOptions?.Fees, Gas: broadcastOptions?.Gas, GasPrices: null, broadcastOptions?.FeesDenoms), cancellationToken).ConfigureAwait(false);

            try
            {
                return await _transactionsApi.BroadcastTransactionAsync(signedTransaction.ToSerialization(), cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                Sequence++;
            }
        }

        public Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionAsyncAndWait(SerializedTransaction transaction, CancellationToken cancellationToken = default)
        {
            try
            {
                return _transactionsApi.BroadcastTransactionAsync(transaction.ToSerialization(), cancellationToken);
            }
            finally
            {
                Sequence++;
            }
        }
    }
}
