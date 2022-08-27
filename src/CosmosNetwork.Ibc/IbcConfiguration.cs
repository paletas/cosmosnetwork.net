using CosmosNetwork.Ibc;

namespace CosmosNetwork
{
    public static class IbcConfiguration
    {
        public static CosmosNetworkConfigurator AddIbc(this CosmosNetworkConfigurator configurator)
        {
            configurator.AddModule(new IbcModule());

            return configurator;
        }
    }
}
