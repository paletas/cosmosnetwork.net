namespace CosmosNetwork.Modules.Mint.Serialization
{
    internal class Minter
    {
        public decimal Inflation { get; set; }

        public decimal AnnualProvisions { get; set; }

        public Mint.Minter ToModel()
        {
            return new Mint.Minter(this.Inflation, this.AnnualProvisions);
        }
    }
}
