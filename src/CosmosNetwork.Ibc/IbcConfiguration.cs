namespace CosmosNetwork.Ibc
{
    public static class IbcConfiguration
    {
        public static CosmosNetworkConfigurator AddIbc(this CosmosNetworkConfigurator configurator)
        {
            configurator.AddMessageModule<IbcModule>();

            return configurator;
        }
    }
}
