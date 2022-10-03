using CosmosNetwork.CosmWasm;
using CosmosNetwork.Ibc;

namespace CosmosNetwork
{
    public static class TerraConfigurator
    {
        public static void SetupTerra(this CosmosNetworkConfigurator configurator, string chainId)
        {
            if (configurator.Network is not null)
                throw new InvalidOperationException("already has network configured");

            configurator.SetupChain(new TerraOptions(chainId)); 
            configurator.AddCosmWasm()
                .AddIbc();
        }
    }
}