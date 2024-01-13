namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    public record DelegationParams(
        TimeSpan UnbondingTime,
        uint MaxValidators,
        uint MaxEntries,
        ulong HistoricalEntries,
        string BondDenom)
    {
        public Staking.DelegationParams ToModel()
        {
            return new Staking.DelegationParams(this.UnbondingTime, this.MaxValidators, this.MaxEntries, this.HistoricalEntries, this.BondDenom);
        }
    }
}
