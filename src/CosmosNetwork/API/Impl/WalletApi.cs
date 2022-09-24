using CosmosNetwork.Keys;
using CosmosNetwork.Serialization.Json.Responses;
using CosmosNetwork.Wallets;
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

        public ValueTask<IWallet> GetWallet(string mnemonicKey, MnemonicKeyOptions keyOptions, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<IWallet>(new DirectWallet(Options, new MnemonicKey(mnemonicKey, keyOptions), this, _transactionsApi));
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
                : new AccountBalances(accountBalances.Balances.Select(bal => new NativeCoin(bal.Denom, bal.Amount)));
        }
    }
}
