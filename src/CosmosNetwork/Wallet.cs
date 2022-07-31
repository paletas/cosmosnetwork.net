using CosmosNetwork.API;
using CosmosNetwork.Keys;

namespace CosmosNetwork
{
    internal class Wallet : IWallet
    {
        private readonly CosmosApiOptions _options;
        private readonly IWalletApi _walletApi;
        private readonly ITransactionsApi _transactionsApi;
        private MnemonicKey? _key;

        public Wallet(CosmosApiOptions options, IWalletApi walletApi, ITransactionsApi transactionsApi)
        {
            _options = options;
            _walletApi = walletApi;
            _transactionsApi = transactionsApi;
        }

        public PublicKey? PublicKey { get { return _key?.PublicKey; } }

        public string? AccountAdddress { get { return PublicKey?.Address; } }

        internal string? AccountNumber { get; private set; }

        internal ulong? Sequence { get; private set; }

        public Task SetKey(string mnemonicKey, CancellationToken cancellationToken = default)
        {
            _key = new MnemonicKey(mnemonicKey);

            if (AccountAdddress == null) throw new InvalidOperationException("Unable to determine Account Address");

            return Task.CompletedTask;
        }

        public async Task<AccountInformation?> GetAccountInformation(CancellationToken cancellationToken = default)
        {
            if (AccountAdddress == null) throw new InvalidOperationException("Unable to determine Account Address");
            var accountInformation = await _walletApi.GetAccountInformation(AccountAdddress, cancellationToken).ConfigureAwait(false);
            if (accountInformation != null)
            {
                AccountNumber = accountInformation.AccountNumber;
                Sequence = accountInformation.AccountSequence;
            }
            return accountInformation;
        }

        public Task<AccountBalances> GetBalances(CancellationToken cancellationToken = default)
        {
            if (AccountAdddress == null) throw new InvalidOperationException("Unable to determine Account Address");
            return _walletApi.GetAccountBalances(AccountAdddress, cancellationToken);
        }

        public async Task<SignedTransaction> CreateSignedTransaction(IEnumerable<Message> messages, CreateTransactionOptions transactionOptions, CancellationToken cancellationToken = default)
        {
            if (_key == null) throw new InvalidOperationException("Need to setup wallet key first");

            if (PublicKey == null) throw new InvalidOperationException("Private key needs to be set");
            if (AccountNumber == null || Sequence == null)
            {
                await GetAccountInformation(cancellationToken).ConfigureAwait(false);

                if (AccountNumber == null) throw new InvalidOperationException("AccountNumber needs to be set");
                if (Sequence == null) throw new InvalidOperationException("AccountSequence needs to be set");
            }

            var signers = new SignerOptions[] { new(AccountNumber, Sequence.Value, PublicKey) };

            throw new NotImplementedException();
        }

        public async Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(IEnumerable<Message> messages, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default)
        {
            if (PublicKey == null) throw new InvalidOperationException("Private key needs to be set");
            if (AccountNumber == null || Sequence == null)
            {
                await GetAccountInformation(cancellationToken).ConfigureAwait(false);

                if (AccountNumber == null) throw new InvalidOperationException("AccountNumber needs to be set");
                if (Sequence == null) throw new InvalidOperationException("AccountSequence needs to be set");
            }

            return await _transactionsApi.SimulateTransaction(
                messages,
                new[] { new SignerOptions(AccountNumber, Sequence.Value, PublicKey) },
                simulationOptions
            ).ConfigureAwait(false);
        }

        public Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            return _transactionsApi.SimulateTransaction(transaction, cancellationToken);
        }

        public async Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionBlock(IEnumerable<Message> messages, BroadcastTransactionOptions? broadcastOptions = null, CancellationToken cancellationToken = default)
        {
            var signedTransaction = await CreateSignedTransaction(messages, new CreateTransactionOptions(broadcastOptions?.Memo, broadcastOptions?.TimeoutHeight, broadcastOptions?.Fees, Gas: broadcastOptions?.Gas, GasPrices: null, broadcastOptions?.FeesDenoms), cancellationToken).ConfigureAwait(false);

            try
            {
                return await _transactionsApi.BroadcastTransactionBlock(signedTransaction, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                Sequence++;
            }
        }

        public Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionBlock(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            try
            {
                return _transactionsApi.BroadcastTransactionBlock(transaction, cancellationToken);
            }
            finally
            {
                Sequence++;
            }
        }

        public async Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionAsyncAndWait(IEnumerable<Message> messages, BroadcastTransactionOptions? broadcastOptions = null, CancellationToken cancellationToken = default)
        {
            var signedTransaction = await CreateSignedTransaction(messages, new CreateTransactionOptions(broadcastOptions?.Memo, broadcastOptions?.TimeoutHeight, broadcastOptions?.Fees, Gas: broadcastOptions?.Gas, GasPrices: null, broadcastOptions?.FeesDenoms), cancellationToken).ConfigureAwait(false);

            try
            {
                return await _transactionsApi.BroadcastTransactionAsyncAndWait(signedTransaction, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                Sequence++;
            }
        }

        public Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionAsyncAndWait(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            try
            {
                return _transactionsApi.BroadcastTransactionAsyncAndWait(transaction, cancellationToken);
            }
            finally
            {
                Sequence++;
            }
        }

        public async Task<Fee> EstimateFee(IEnumerable<Message> messages, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            if (PublicKey == null) throw new InvalidOperationException("Private key needs to be set");
            if (AccountNumber == null || Sequence == null)
            {
                await GetAccountInformation(cancellationToken).ConfigureAwait(false);

                if (AccountNumber == null) throw new InvalidOperationException("AccountNumber needs to be set");
                if (Sequence == null) throw new InvalidOperationException("AccountSequence needs to be set");
            }

            return await _transactionsApi.EstimateFee(
                messages,
                new[] { new SignerOptions(AccountNumber, Sequence.Value, PublicKey) },
                estimateOptions
            ).ConfigureAwait(false);
        }
    }
}
