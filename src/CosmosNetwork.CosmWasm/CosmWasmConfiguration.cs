using CosmosNetwork.CosmWasm;

namespace CosmosNetwork
{
    public static class CosmWasmConfiguration
    {
        public static CosmosNetworkConfigurator AddCosmWasm(this CosmosNetworkConfigurator configurator)
        {
            configurator.AddModule(new WasmModule());

            return configurator;
        }
    }
}
