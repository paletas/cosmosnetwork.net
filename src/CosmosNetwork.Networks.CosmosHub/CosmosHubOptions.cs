namespace CosmosNetwork
{
    public record CosmosHubOptions(string ChainId) : NetworkOptions(ChainId, COIN_TYPE, ADDRESS_PREFIX, new[] { DENOM })
    {
        private const string COIN_TYPE = "118";
        private const string ADDRESS_PREFIX = "cosmos";
        private const string DENOM = "uatom";
    }
}
