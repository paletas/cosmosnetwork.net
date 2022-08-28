using CosmosNetwork.CosmWasm;

namespace CosmosNetwork
{
    public static class TerraConfigurator
    {
        public static CosmosNetworkConfigurator SetupTerra(this CosmosNetworkConfigurator configurator)
        {
            return configurator.AddCosmWasm().AddIbc();
        }
    }
}