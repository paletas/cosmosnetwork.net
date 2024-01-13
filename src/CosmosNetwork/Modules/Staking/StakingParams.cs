namespace CosmosNetwork.Modules.Staking
{
    public record StakingParams(
        TimeSpan UnbondingTime,
        int MaxValidators,
        int MaxEntries,
        int HistoricalEntries,
        string BondDenom)
    {
        internal Serialization.StakingParams ToSerialization()
        {
            return new Serialization.StakingParams
            {
                UnbondingTime = this.UnbondingTime,
                MaxValidators = this.MaxValidators,
                MaxEntries = this.MaxEntries,
                HistoricalEntries = this.HistoricalEntries,
                BondDenom = this.BondDenom
            };
        }
    }
}
