namespace Terra.NET.API
{
    public interface IWalletApi
    {
        Task<IWallet> GetWallet(string mnemonicKey, CancellationToken cancellationToken = default);

        Task<AccountInformation?> GetAccountInformation(string accountAddress, CancellationToken cancellationToken = default);

        Task<AccountBalances> GetAccountBalances(string accountAddress, CancellationToken cancellationToken = default);
    }
}
