namespace CosmosNetwork
{
    public static class CosmosHubConfigurator
    {
        public static CosmosNetworkConfigurator SetupCosmosHub(this CosmosNetworkConfigurator configurator)
        {
            return configurator.AddIbc();
        }
    }
}