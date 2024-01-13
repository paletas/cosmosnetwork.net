namespace CosmosNetwork.Modules.Mint.Serialization
{
    internal class MintParams
    {
        public decimal InflationMax { get; set; }

        public decimal InflationMin { get; set; }

        public decimal InflationRateChange { get; set; }

        public string MintDenom { get; set; }

        public decimal GoalBonded { get; set; }

        public long BlocksPerYear { get; set; }

        public Mint.MintParams ToModel()
        {
            return new Mint.MintParams(
                this.InflationMax,
                this.InflationMin,
                this.InflationRateChange,
                this.MintDenom,
                this.GoalBonded,
                this.BlocksPerYear);
        }
    }
}
