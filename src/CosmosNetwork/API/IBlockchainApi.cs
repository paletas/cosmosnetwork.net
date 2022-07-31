namespace CosmosNetwork.API
{
    public interface IBlockchainApi
    {
        Task<IReadOnlyDictionary<string, decimal>> GetGasPrices(CancellationToken cancellationToken = default);

        Task<DenomMetadata[]> GetNativeDenoms(CancellationToken cancellationToken = default);

        Task<DenomSwapRate[]> GetSwapRates(string denomFrom, CancellationToken cancellationToken = default);

        Task<DenomSwapRate> GetSwapRate(string denomFrom, string denomTo, CancellationToken cancellationToken = default);

        Task<Coin> SimulateSwap(string denomFrom, ulong amountFrom, string denomTo, CancellationToken cancellationToken = default);
    }
}
