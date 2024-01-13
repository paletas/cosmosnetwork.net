namespace CosmosNetwork.Modules.Distribution
{
    public record FeePool(CoinDecimal[] CommunityPool)
    {
        internal Serialization.FeePool ToSerialization()
        {
            return new Serialization.FeePool
            {
                CommunityPool = this.CommunityPool.Select(c => c.ToSerialization()).ToArray(),
            };
        }
    }
}
