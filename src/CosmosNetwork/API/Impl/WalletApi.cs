using CosmosNetwork.Keys.Sources;
using CosmosNetwork.Serialization.Json.Responses;
using CosmosNetwork.Wallets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CosmosNetwork.API.Impl
{
  internal class WalletApi : CosmosApiModule, IWalletApi
    {
        private readonly NetworkOptions _networkOptions;
        private readonly ITransactionsApi _transactionsApi;

        public WalletApi(
            [ServiceKey] string servicesKey,
            IServiceProvider serviceProvider,
            IHttpClientFactory httpClientFactory,
            ILogger<WalletApi> logger) : base(servicesKey, serviceProvider, httpClientFactory, logger)
        {
            this._networkOptions = serviceProvider.GetRequiredKeyedService<NetworkOptions>(servicesKey);
            this._transactionsApi = serviceProvider.GetRequiredKeyedService<ITransactionsApi>(servicesKey);
        }

        public ValueTask<IWallet> GetWallet(string mnemonicKey, MnemonicKeyOptions keyOptions, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<IWallet>(new DirectWallet(this.Options, new MnemonicKeySource(mnemonicKey, keyOptions, this._networkOptions), this, this._transactionsApi));
        }

        public async Task<AccountInformation?> GetAccountInformation(string accountAddress, CancellationToken cancellationToken = default)
        {
            GetAccountInformationResponse? accountInformation = await Get<GetAccountInformationResponse>($"/cosmos/auth/v1beta1/accounts/{accountAddress}", cancellationToken).ConfigureAwait(false);
            return accountInformation == null
                ? null
                : new AccountInformation(accountInformation.Account.AccountNumber, accountInformation.Account.Sequence);
        }

        public async Task<AccountBalances> GetAccountBalances(string accountAddress, CancellationToken cancellationToken = default)
        {
            Serialization.Json.AccountBalances? accountBalances = await Get<Serialization.Json.AccountBalances>($"/cosmos/bank/v1beta1/balances/{accountAddress}", cancellationToken).ConfigureAwait(false);
            return accountBalances == null
                ? throw new InvalidOperationException()
                : new AccountBalances(accountBalances.Balances.Select(bal =>
                {
                    return UInt128.TryParse(bal.Amount, out UInt128 amount) == false
                        ? throw new InvalidOperationException($"amount format is invalid: {bal.Amount}")
                        : new NativeCoin(bal.Denom, amount);
                }));
        }
    }
}
