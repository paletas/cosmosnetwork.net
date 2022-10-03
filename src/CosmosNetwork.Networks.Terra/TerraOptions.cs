namespace CosmosNetwork
{
    public record TerraOptions(string ChainId) : NetworkOptions(ChainId, TerraOptions._CoinType, TerraOptions._AddressPrefix)
    {
        private const string _CoinType = "330";
        private const string _AddressPrefix = "terra";
    }
}
