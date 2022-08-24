﻿namespace CosmosNetwork.CosmWasm
{
    public static class CosmWasmConfiguration
    {
        public static CosmosNetworkConfigurator AddCosmWasm(this CosmosNetworkConfigurator configurator)
        {
            configurator.AddModule<WasmModule>();

            return configurator;
        }
    }
}