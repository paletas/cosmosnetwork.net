namespace CosmosNetwork.CosmWasm
{
    public static class CosmWasmConfiguration
    {
        public static CosmosNetworkConfigurator AddCosmWasm(this CosmosNetworkConfigurator configurator)
        {
            _ = configurator.AddMessageModule<WasmModule>();

            return configurator;
        }
    }
}
