using CosmosNetwork.Ibc;

namespace CosmosNetwork
{
    public static class CosmosHubConfigurator
    {
        public static void SetupCosmosHub(this CosmosNetworkConfigurator configurator, string chainId)
        {
            if (configurator.Network is not null)
                throw new InvalidOperationException("already has network configured");

            configurator.SetupChain(new CosmosHubOptions(chainId));
            
            configurator.AddIbc();
        }
    }
}