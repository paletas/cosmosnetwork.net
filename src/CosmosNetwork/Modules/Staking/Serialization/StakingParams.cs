namespace CosmosNetwork.Modules.Staking.Serialization
{
    public class StakingParams
    {
        public TimeSpan UnbondingTime { get; set; }

        public int MaxValidators { get; set; }

        public int MaxEntries { get; set; }

        public int HistoricalEntries { get; set; }

        public string BondDenom { get; set; }

        public Staking.StakingParams ToModel()
        {
            return new Staking.StakingParams(
                this.UnbondingTime,
                this.MaxValidators,
                this.MaxEntries,
                this.HistoricalEntries,
                this.BondDenom);
        }
    }
}
