using Google.Protobuf;
using Terra.NET.API;
using Terra.NET.API.Internal;
using Terra.NET.Keys;

namespace Terra.NET
{
    internal class Wallet : IWallet
    {
        private readonly TerraApiOptions _options;
        private readonly IWalletApi _walletApi;
        private readonly ITransactionsApi _transactionsApi;
        private readonly TransactionsBuilder _transactionsBuilder;
        private MnemonicKey? _key;

        public Wallet(TerraApiOptions options, IWalletApi walletApi, ITransactionsApi transactionsApi, TransactionsBuilder transactionsBuilder)
        {
            this._options = options;
            this._walletApi = walletApi;
            this._transactionsApi = transactionsApi;
            this._transactionsBuilder = transactionsBuilder;
        }

        public PublicKey? PublicKey { get { return this._key?.PublicKey; } }

        public string? AccountAdddress { get { return this.PublicKey?.Address; } }

        internal string? AccountNumber { get; private set; }

        internal ulong? Sequence { get; private set; }

        public Task SetKey(string mnemonicKey, CancellationToken cancellationToken = default)
        {
            this._key = new MnemonicKey(mnemonicKey);

            if (this.AccountAdddress == null) throw new InvalidOperationException("Unable to determine Account Address");

            return Task.CompletedTask;
        }

        public async Task<AccountInformation?> GetAccountInformation(CancellationToken cancellationToken = default)
        {
            if (this.AccountAdddress == null) throw new InvalidOperationException("Unable to determine Account Address");
            var accountInformation = await this._walletApi.GetAccountInformation(this.AccountAdddress, cancellationToken).ConfigureAwait(false);
            if (accountInformation != null)
            {
                this.AccountNumber = accountInformation.AccountNumber;
                this.Sequence = accountInformation.AccountSequence;
            }
            return accountInformation;
        }

        public Task<AccountBalances> GetBalances(CancellationToken cancellationToken = default)
        {
            if (this.AccountAdddress == null) throw new InvalidOperationException("Unable to determine Account Address");
            return this._walletApi.GetAccountBalances(this.AccountAdddress, cancellationToken);
        }

        public async Task<SignedTransaction> CreateSignedTransaction(IEnumerable<Message> messages, CreateTransactionOptions transactionOptions, CancellationToken cancellationToken)
        {
            if (this._key == null) throw new InvalidOperationException("Need to setup wallet key first");

            if (this.PublicKey == null) throw new InvalidOperationException("Private key needs to be set");
            if (this.AccountNumber == null || this.Sequence == null)
            {
                await GetAccountInformation(cancellationToken).ConfigureAwait(false);

                if (this.AccountNumber == null) throw new InvalidOperationException("AccountNumber needs to be set");
                if (this.Sequence == null) throw new InvalidOperationException("AccountSequence needs to be set");
            }

            var signers = new SignerOptions[] { new(this.AccountNumber, this.Sequence.Value, this.PublicKey) };

            var transaction = this._transactionsBuilder.CreateTransaction(
                messages,
                transactionOptions.Memo,
                transactionOptions.TimeoutHeight
            );

            Fee fees;
            if (transactionOptions.Fees == null)
            {
                fees = await this._transactionsApi.EstimateFee(messages, signers, new EstimateFeesOptions(transactionOptions.Memo, FeeDenoms: transactionOptions.FeesDenoms ?? _options.DefaultDenoms), cancellationToken).ConfigureAwait(false);
            }
            else
            {
                fees = transactionOptions.Fees;
            }

            var taxAmount = await this._transactionsApi.ComputeTax(messages, signers, cancellationToken);

            fees = fees with
            {
                Amount = fees.Amount.Select(feeDenom =>
                {
                    var denom = feeDenom.Denom;
                    var tax = taxAmount.SingleOrDefault(tax => tax.Denom == denom);

                    if (tax == null) return feeDenom;
                    else return feeDenom with { Amount = feeDenom.Amount + tax.Amount };
                }).ToArray()
            };

            transaction.AuthInfo.Fee = new Cosmos.SDK.Protos.Tx.Fee
            {
                GasLimit = fees.GasLimit
            };
            transaction.AuthInfo.Fee.Amount.Add(fees.Amount.Select(c => new Cosmos.SDK.Protos.Coin { Denom = c.Denom, Amount = c.Amount.ToString() }));

            var signedTx = this._key.SignTransaction(transaction, signers, new SignOptions(this._options.ChainId));
            return new SignedTransaction(signedTx.ToByteArray(), fees);
        }

        public async Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(IEnumerable<Message> messages, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default)
        {
            if (this.PublicKey == null) throw new InvalidOperationException("Private key needs to be set");
            if (this.AccountNumber == null || this.Sequence == null)
            {
                await GetAccountInformation().ConfigureAwait(false);

                if (this.AccountNumber == null) throw new InvalidOperationException("AccountNumber needs to be set");
                if (this.Sequence == null) throw new InvalidOperationException("AccountSequence needs to be set");
            }

            return await this._transactionsApi.SimulateTransaction(
                messages,
                new[] { new SignerOptions(this.AccountNumber, this.Sequence.Value, this.PublicKey) },
                simulationOptions
            ).ConfigureAwait(false);
        }

        public Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            return this._transactionsApi.SimulateTransaction(transaction, cancellationToken);
        }

        public async Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransaction(IEnumerable<Message> messages, BroadcastTransactionOptions? broadcastOptions = null, CancellationToken cancellationToken = default)
        {
            var signedTransaction = await CreateSignedTransaction(messages, new CreateTransactionOptions(broadcastOptions?.Memo, broadcastOptions?.TimeoutHeight, broadcastOptions?.Fees, GasPrices: null, broadcastOptions?.FeesDenoms), cancellationToken).ConfigureAwait(false);

            return await this._transactionsApi.BroadcastTransaction(signedTransaction, cancellationToken).ConfigureAwait(false);
        }

        public Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransaction(SignedTransaction transaction, CancellationToken cancellationToken = default)
        {
            return this._transactionsApi.BroadcastTransaction(transaction, cancellationToken);
        }

        public async Task<Fee> EstimateFee(IEnumerable<Message> messages, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default)
        {
            if (this.PublicKey == null) throw new InvalidOperationException("Private key needs to be set");
            if (this.AccountNumber == null || this.Sequence == null)
            {
                await GetAccountInformation().ConfigureAwait(false);

                if (this.AccountNumber == null) throw new InvalidOperationException("AccountNumber needs to be set");
                if (this.Sequence == null) throw new InvalidOperationException("AccountSequence needs to be set");
            }

            return await this._transactionsApi.EstimateFee(
                messages,
                new[] { new SignerOptions(this.AccountNumber, this.Sequence.Value, this.PublicKey) },
                estimateOptions
            ).ConfigureAwait(false);
        }
    }
}
