using CosmosNetwork.Ibc;

namespace CosmosNetwork.CosmWasm
{
    public static class IbcConfiguration
    {
        public static CosmosNetworkConfigurator AddIbc(this CosmosNetworkConfigurator configurator)
        {
            configurator.AddModule<IbcModule>(new IbcModule());

            return configurator;
        }
    }
}
