namespace CosmosNetwork.Ibc
{
    public static class IbcConfiguration
    {
        public static CosmosNetworkConfigurator AddIbc(this CosmosNetworkConfigurator configurator)
        {
            _ = configurator.AddMessageModule(new IbcModule());

            return configurator;
        }
    }
}
