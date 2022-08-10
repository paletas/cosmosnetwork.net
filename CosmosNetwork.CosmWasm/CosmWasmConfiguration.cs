using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.CosmWasm
{
    public static class CosmWasmConfiguration
    {
        public static CosmosNetworkConfigurator AddCosmWasm(this CosmosNetworkConfigurator configurator)
        {
            configurator.Registry.RegisterMessages(typeof(CosmWasmConfiguration).Assembly);

            return configurator;
        }
    }
}
