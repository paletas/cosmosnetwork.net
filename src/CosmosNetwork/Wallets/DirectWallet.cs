using CosmosNetwork.API;
using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Wallets
{
    internal class DirectWallet : IWallet
    {
        private readonly CosmosApiOptions _options;
        private readonly IWalletApi _walletApi;
        private readonly ITransactionsApi _transactionsApi;
        private readonly IKeySource _keySource;

        public DirectWallet(CosmosApiOptions options, IKeySource keySource, IWalletApi walletApi, ITransactionsApi transactionsApi)
        {
            this._options = options;
            this._walletApi = walletApi;
            this._transactionsApi = transactionsApi;
            this._keySource = keySource;
        }

        public IPublicKey PublicKey => this._keySource.PublicKey;

        public CosmosAddress? AccountAddress => this.PublicKey is null ? null : (CosmosAddress)this.PublicKey.AsAddress(this._options.Network?.AddressPrefix ?? throw new InvalidOperationException());

        public string? AccountNumber { get; private set; }

        public ulong? Sequence { get; private set; }

        protected bool RequiresAccountUpdate { get; private set; } = true;

        public async Task<AccountInformation?> GetAccountInformation(CancellationToken cancellationToken = default)
        {
            if (this.AccountAddress == null)
            {
                throw new InvalidOperationException("Unable to determine Account Address");
            }

            AccountInformation? accountInformation = await this._walletApi.GetAccountInformation(this.AccountAddress, cancellationToken).ConfigureAwait(false);
            if (accountInformation != null)
            {
                this.AccountNumber = accountInformation.AccountNumber;
                this.Sequence = accountInformation.AccountSequence;
            }
            return accountInformation;
        }

        public async Task UpdateAccountInformation(CancellationToken cancellationToken = default)
        {
            AccountInformation? accountInformation = await GetAccountInformation(cancellationToken);

            if (accountInformation is null)
            {
                throw new InvalidOperationException();
            }

            this.AccountNumber = accountInformation.AccountNumber;
            this.Sequence = accountInformation.AccountSequence;
            this.RequiresAccountUpdate = false;
        }

        public Task<AccountBalances> GetBalances(CancellationToken cancellationToken = default)
        {
            return this.AccountAddress == null
                ? throw new InvalidOperationException("Unable to determine Account Address")
                : this._walletApi.GetAccountBalances(this.AccountAddress, cancellationToken);
        }

        public async Task<SerializedTransaction> CreateSignedTransaction(IEnumerable<Message> messages, CreateTransactionOptions? transactionOptions = null, CancellationToken cancellationToken = default)
        {
            if (this.RequiresAccountUpdate)
            {
                await UpdateAccountInformation(cancellationToken).ConfigureAwait(false);
            }

            Serialization.Fee? fees = transactionOptions?.Fees?.ToSerialization();

            Serialization.Proto.Transaction transaction = new(
                new Serialization.Proto.TransactionBody(messages.Select(msg => Any.Pack(msg.ToSerialization())).ToArray(), transactionOptions?.TimeoutHeight, transactionOptions?.Memo),
                new Serialization.Proto.AuthInfo(new List<Serialization.SignatureDescriptor>(), fees ?? new Serialization.Fee()),
                new List<byte[]>());

            byte[] serializedTx = transaction.GetBytes();
            return new SerializedTransaction(serializedTx, transactionOptions?.Fees);
        }

        public async Task<(uint? ErrorCode, TransactionSimulationResults? Result)> SimulateTransaction(IEnumerable<Message> messages, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default)
        {
            if (this.RequiresAccountUpdate)
            {
                await UpdateAccountInformation(cancellationToken).ConfigureAwait(false);
            }

            Serialization.Fee? fees = simulationOptions?.Fees?.ToSerialization();

            Serialization.SignatureDescriptor signatureDescriptor = new()
            {
                PublicKey = this.PublicKey.ToProto(),
                Data = new Serialization.SignatureMode
                {
                    Single = new Serialization.SingleMode(Serialization.SignModeEnum.Direct)
                },
                Sequence = this.Sequence!.Value
            };

            Serialization.Proto.Transaction transaction = new(
                new Serialization.Proto.TransactionBody(messages.Select(msg => Any.Pack(msg.ToSerialization())).ToArray(), simulationOptions?.TimeoutHeight, simulationOptions?.Memo),
                new Serialization.Proto.AuthInfo(new List<Serialization.SignatureDescriptor>() { signatureDescriptor }, fees ?? new Serialization.Fee()),
                new List<byte[]>() { Array.Empty<byte>() });

            return await this._transactionsApi.SimulateTransaction(transaction, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Fee> EstimateFee(IEnumerable<Message> messages, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            Serialization.Fee? fees = estimateOptions?.Fees?.ToSerialization();
            if (fees is null)
            {
                throw new InvalidOperationException();
            }

            Serialization.Proto.Transaction transaction = new(
                new Serialization.Proto.TransactionBody(messages.Select(msg => Any.Pack(msg.ToSerialization())).ToArray(), estimateOptions?.TimeoutHeight, estimateOptions?.Memo),
                new Serialization.Proto.AuthInfo(new List<Serialization.SignatureDescriptor>(), fees),
                new List<byte[]>());

            return await this._transactionsApi.EstimateFee(transaction, estimateOptions, cancellationToken).ConfigureAwait(false);
        }

        public Task<(uint? ErrorCode, TransactionSimulationResults? Result)> SimulateTransaction(SerializedTransaction transaction, CancellationToken cancellationToken = default)
        {
            return this._transactionsApi.SimulateTransaction(transaction.ToSerialization(), cancellationToken);
        }

        public async Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionBlock(IEnumerable<Message> messages, BroadcastTransactionOptions broadcastOptions, CancellationToken cancellationToken = default)
        {
            SerializedTransaction signedTransaction = await CreateSignedTransaction(messages, new CreateTransactionOptions(broadcastOptions.Fees, broadcastOptions.Memo, broadcastOptions.TimeoutHeight, Gas: broadcastOptions.Gas, GasPrices: null, broadcastOptions?.FeesDenoms), cancellationToken).ConfigureAwait(false);

            try
            {
                return await this._transactionsApi.BroadcastTransaction(signedTransaction.ToSerialization(), cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                this.Sequence++;
            }
        }

        public Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionBlock(SerializedTransaction transaction, CancellationToken cancellationToken = default)
        {
            try
            {
                return this._transactionsApi.BroadcastTransaction(transaction.ToSerialization(), cancellationToken);
            }
            finally
            {
                this.Sequence++;
            }
        }

        public async Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionAsyncAndWait(IEnumerable<Message> messages, BroadcastTransactionOptions broadcastOptions, CancellationToken cancellationToken = default)
        {
            SerializedTransaction signedTransaction = await CreateSignedTransaction(messages, broadcastOptions, cancellationToken).ConfigureAwait(false);

            try
            {
                return await this._transactionsApi.BroadcastTransactionAsync(signedTransaction.ToSerialization(), cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                this.Sequence++;
            }
        }

        public Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionAsyncAndWait(SerializedTransaction transaction, CancellationToken cancellationToken = default)
        {
            try
            {
                return this._transactionsApi.BroadcastTransactionAsync(transaction.ToSerialization(), cancellationToken);
            }
            finally
            {
                this.Sequence++;
            }
        }
    }
}
