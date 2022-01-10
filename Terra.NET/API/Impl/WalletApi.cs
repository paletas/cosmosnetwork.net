using Microsoft.Extensions.Logging;

namespace Terra.NET.API.Impl
{
    internal class WalletApi : BaseApiSection, IWalletApi
    {
        private readonly ITransactionsApi _transactionsApi;

        public WalletApi(TerraApiOptions options, HttpClient httpClient, ILogger<WalletApi> logger, ITransactionsApi transactionsApi) : base(options, httpClient, logger)
        {
            this._transactionsApi = transactionsApi;
        }

        public async Task<IWallet> GetWallet(string mnemonicKey, CancellationToken cancellationToken = default)
        {
            var wallet = new Wallet(this, this._transactionsApi);
            await wallet.SetKey(mnemonicKey, cancellationToken).ConfigureAwait(false);

            return wallet;
        }

        public async Task<AccountInformation?> GetAccountInformation(string accountAddress, CancellationToken cancellationToken = default)
        {
            var accountInformation = await Get<Serialization.Json.Responses.GetAccountInformationResponse>($"/cosmos/auth/v1beta1/accounts/{accountAddress}", cancellationToken).ConfigureAwait(false);
            if (accountInformation == null) return null;
            return new AccountInformation(accountInformation.Account.AccountNumber, accountInformation.Account.Sequence);
        }

        public async Task<AccountBalances> GetAccountBalances(string accountAddress, CancellationToken cancellationToken = default)
        {
            var accountBalances = await Get<Serialization.Json.AccountBalances>($"/cosmos/bank/v1beta1/balances/{accountAddress}", cancellationToken).ConfigureAwait(false);
            if (accountBalances == null) throw new InvalidOperationException();
            return new AccountBalances(accountBalances.Balances.Select(bal => new NativeCoin(bal.Denom, bal.Amount)));
        }
    }
}
