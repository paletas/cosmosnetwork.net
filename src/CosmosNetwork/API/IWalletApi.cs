using CosmosNetwork.Keys;

namespace CosmosNetwork.API
{
    public interface IWalletApi
    {
        ValueTask<IWallet> GetWallet(string mnemonicKey, MnemonicKeyOptions keyOptions, CancellationToken cancellationToken = default);

        Task<AccountInformation?> GetAccountInformation(string accountAddress, CancellationToken cancellationToken = default);

        Task<AccountBalances> GetAccountBalances(string accountAddress, CancellationToken cancellationToken = default);
    }
}
