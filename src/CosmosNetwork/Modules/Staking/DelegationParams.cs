namespace CosmosNetwork.Modules.Staking
{
    public record DelegationParams(
        TimeSpan UnbondingTime,
        uint MaxValidators,
        uint MaxEntries,
        ulong HistoricalEntries,
        string BondDenom);
}
