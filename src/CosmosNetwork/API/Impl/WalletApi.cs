using CosmosNetwork.Serialization.Json.Responses;
using Microsoft.Extensions.Logging;

namespace CosmosNetwork.API.Impl
{
    internal class WalletApi : BaseApiSection, IWalletApi
    {
        private readonly ITransactionsApi _transactionsApi;

        public WalletApi(CosmosApiOptions options, HttpClient httpClient, ILogger<WalletApi> logger, ITransactionsApi transactionsApi) : base(options, httpClient, logger)
        {
            _transactionsApi = transactionsApi;
        }

        public async Task<IWallet> GetWallet(string mnemonicKey, CancellationToken cancellationToken = default)
        {
            var wallet = new Wallet(Options, this, _transactionsApi);
            await wallet.SetKey(mnemonicKey, cancellationToken).ConfigureAwait(false);

            return wallet;
        }

        public async Task<AccountInformation?> GetAccountInformation(string accountAddress, CancellationToken cancellationToken = default)
        {
            var accountInformation = await Get<GetAccountInformationResponse>($"/cosmos/auth/v1beta1/accounts/{accountAddress}", cancellationToken).ConfigureAwait(false);
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
