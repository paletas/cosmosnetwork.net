using CosmosNetwork.CosmWasm.API;
using CosmosNetwork.CosmWasm.API.Impl;

namespace CosmosNetwork.CosmWasm
{
    public static class CosmWasmConfiguration
    {
        public static CosmosNetworkConfigurator AddCosmWasm(this CosmosNetworkConfigurator configurator)
        {
            configurator.AddMessageModule<WasmModule>();
            configurator.AddApiModule<ICosmWasmApi, CosmWasmApi>();

            configurator.SetupModuleClient<CosmWasmCosmosApi>();

            return configurator;
        }
    }
}
