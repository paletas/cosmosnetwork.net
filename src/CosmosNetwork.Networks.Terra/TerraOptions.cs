namespace CosmosNetwork
{
    public record TerraOptions(string ChainId) : NetworkOptions(ChainId, COIN_TYPE, ADDRESS_PREFIX, new[] { DENOM } )
    {
        public const string COIN_TYPE = "330";
        public const string ADDRESS_PREFIX = "terra";
        public const string DENOM = "uluna";
    }
}
