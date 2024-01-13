namespace CosmosNetwork.Modules.Mint
{
    public record MintParams(
        decimal InflationMax,
        decimal InflationMin,
        decimal InflationRateChange,
        string MintDenom,
        decimal GoalBonded,
        long BlocksPerYear)
    {
        internal Serialization.MintParams ToSerialization()
        {
            return new Serialization.MintParams
            {
                InflationMax = this.InflationMax,
                InflationMin = this.InflationMin,
                InflationRateChange = this.InflationRateChange,
                MintDenom = this.MintDenom,
                GoalBonded = this.GoalBonded,
                BlocksPerYear = this.BlocksPerYear,
            };
        }
    }
}
