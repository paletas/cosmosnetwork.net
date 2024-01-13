namespace CosmosNetwork.Modules.Mint
{
    public record Minter(decimal Inflation, decimal AnnualProvisions)
    {
        internal Serialization.Minter ToSerialization()
        {
            return new Serialization.Minter
            {
                Inflation = Inflation,
                AnnualProvisions = AnnualProvisions
            };
        }
    }
}
