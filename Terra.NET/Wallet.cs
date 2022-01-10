using Terra.NET.API;

namespace Terra.NET
{
    internal class Wallet : IWallet
    {
        private readonly IWalletApi _walletApi;
        private readonly ITransactionsApi _transactionsApi;
        private MnemonicKey _key;

        public Wallet(IWalletApi walletApi, ITransactionsApi transactionsApi)
        {
            this._walletApi = walletApi;
            this._transactionsApi = transactionsApi;
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
            var accountInformation = await _walletApi.GetAccountInformation(this.AccountAdddress, cancellationToken).ConfigureAwait(false);
            if (accountInformation != null)
            {
                this.AccountNumber = accountInformation.AccountNumber;
                this.Sequence = accountInformation.AccountSequence;
            }
            return accountInformation;
        }

        public Task<AccountBalances> GetBalances(CancellationToken cancellationToken = default)
        {
            if (AccountAdddress == null) throw new InvalidOperationException("Unable to determine Account Address");
            return _walletApi.GetAccountBalances(this.AccountAdddress, cancellationToken);
        }

        public async Task<Transaction> CreateTransaction(IEnumerable<Message> messages, CreateTransactionOptions transactionOptions, CancellationToken cancellationToken = default)
        {
            if (this.PublicKey == null) throw new InvalidOperationException("Private key needs to be set");
            if (this.AccountNumber == null || this.Sequence == null)
            {
                await this.GetAccountInformation(cancellationToken).ConfigureAwait(false);

                if (this.AccountNumber == null) throw new InvalidOperationException("AccountNumber needs to be set");
                if (this.Sequence == null) throw new InvalidOperationException("AccountSequence needs to be set");
            }

            return await _transactionsApi.CreateTransaction(
                messages, 
                new[] { new SignerOptions(this.AccountNumber, this.Sequence.Value, this.PublicKey) }, 
                transactionOptions,
                cancellationToken
            ).ConfigureAwait(false);
        }

        public async Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(IEnumerable<Message> messages, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default)
        {
            if (this.PublicKey == null) throw new InvalidOperationException("Private key needs to be set");
            if (this.AccountNumber == null || this.Sequence == null)
            {
                await this.GetAccountInformation().ConfigureAwait(false);

                if (this.AccountNumber == null) throw new InvalidOperationException("AccountNumber needs to be set");
                if (this.Sequence == null) throw new InvalidOperationException("AccountSequence needs to be set");
            }

            return await _transactionsApi.SimulateTransaction(
                messages,
                new[] { new SignerOptions(this.AccountNumber, this.Sequence.Value, this.PublicKey) },
                simulationOptions
            ).ConfigureAwait(false);
        }

        public async Task<Fee> EstimateFee(IEnumerable<Message> messages, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            if (this.PublicKey == null) throw new InvalidOperationException("Private key needs to be set");
            if (this.AccountNumber == null || this.Sequence == null)
            {
                await this.GetAccountInformation().ConfigureAwait(false);

                if (this.AccountNumber == null) throw new InvalidOperationException("AccountNumber needs to be set");
                if (this.Sequence == null) throw new InvalidOperationException("AccountSequence needs to be set");
            }

            return await _transactionsApi.EstimateFee(
                messages,
                new[] { new SignerOptions(this.AccountNumber, this.Sequence.Value, this.PublicKey) },
                estimateOptions
            ).ConfigureAwait(false);
        }
    }

    internal class MnemonicKey
    {
        private readonly byte[] _publicKey;
        private readonly byte[] _privateKey;

        public MnemonicKey(string mnemonic, MnemonicKeyOptions? options = null)
        {
            if (options == null)
                options = new MnemonicKeyOptions();

            var wallet = new Nethereum.HdWallet.Wallet(mnemonic, string.Empty, GetLunaHdPath(options.CoinType, options.Account, options.Index));
            _privateKey = wallet.GetPrivateKey((int)options.Index);
            _publicKey = Cryptography.ECDSA.Secp256K1Manager.GetPublicKey(_privateKey, true);

            PublicKey = new PublicKey(_publicKey);
        }

        public PublicKey PublicKey { get; private set; }

        public (string SignedMessage, int RecoveryId) Sign(string payload)
        {
            //int recoveryId;
            //var signature = Cryptography.ECDSA.Secp256K1Manager.SignCompact(UTF8Encoding.UTF8.GetBytes(payload), this.PrivateKey, out recoveryId);
            //return (Convert.ToBase64String(signature, Base64FormattingOptions.None), recoveryId);
            throw new InvalidOperationException();
        }

        private static string GetLunaHdPath(string coinType, uint account, uint index)
        {
            return $"m/44'/{coinType}'/{account}'/0/x";
        }
    }

    internal class MnemonicKeyOptions
    {
        public const string LUNA_COINTYPE = "330";

        public string CoinType { get; set; } = LUNA_COINTYPE;

        public uint Account { get; set; } = 0;

        public uint Index { get; set; } = 0;
    }
}
