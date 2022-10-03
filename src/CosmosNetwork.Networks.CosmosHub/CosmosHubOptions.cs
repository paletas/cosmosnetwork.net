namespace CosmosNetwork
{
    public record CosmosHubOptions(string ChainId) : NetworkOptions(ChainId, CosmosHubOptions._CoinType, CosmosHubOptions._AddressPrefix)
    {
        private const string _CoinType = "118";
        private const string _AddressPrefix = "cosmos";
    }
}
