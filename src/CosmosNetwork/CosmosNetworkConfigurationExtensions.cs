using CosmosNetwork.API;
using CosmosNetwork.API.Impl;
using CosmosNetwork.Modules;
using CosmosNetwork.Modules.Authz;
using CosmosNetwork.Modules.Bank;
using CosmosNetwork.Modules.Distribution;
using CosmosNetwork.Modules.FeeGrant;
using CosmosNetwork.Modules.Gov;
using CosmosNetwork.Modules.Params;
using CosmosNetwork.Modules.Slashing;
using CosmosNetwork.Modules.Staking;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork
{
    public static class CosmosNetworkConfigurationExtensions
    {
        public static CosmosNetworkConfigurator AddCosmosNetwork(this IServiceCollection services, string endpoint, CosmosApiOptions? options = null)
        {
            const string DEFAULT_HTTPCLIENT_NAME = "Default";
            return AddCosmosNetwork(services, endpoint, DEFAULT_HTTPCLIENT_NAME, isDefaultClient: true, options);
        }

        public static CosmosNetworkConfigurator AddCosmosNetwork(this IServiceCollection services, string endpoint, string clientName, CosmosApiOptions? options = null)
        {
            return AddCosmosNetwork(services, endpoint, clientName, isDefaultClient: false, options);
        }

        private static CosmosNetworkConfigurator AddCosmosNetwork(this IServiceCollection services, string endpoint, string clientName, bool isDefaultClient, CosmosApiOptions? options = null)
        {
            CosmosMessageRegistry cosmosMessageRegistry = new();
            options ??= new CosmosApiOptions(httpClientName: $"HTTP_{clientName}");
            options.MessageRegistry = cosmosMessageRegistry;

            if (options.SkipHttpClientConfiguration == false)
            {
                services.AddHttpClient(options.HttpClientName, client => client.BaseAddress = new Uri(endpoint));
            }

            CosmosNetworkConfigurator cosmosNetworkConfigurator = new(services, cosmosMessageRegistry, options, isDefaultClient == false, clientName);

            cosmosNetworkConfigurator.AddMessageModule<AuthzModule>();
            cosmosNetworkConfigurator.AddMessageModule<GovModule>();
            cosmosNetworkConfigurator.AddMessageModule<BankModule>();
            cosmosNetworkConfigurator.AddMessageModule<DistributionModule>();
            cosmosNetworkConfigurator.AddMessageModule<FeeGrantModule>();
            cosmosNetworkConfigurator.AddMessageModule<SlashingModule>();
            cosmosNetworkConfigurator.AddMessageModule<StakingModule>();
            cosmosNetworkConfigurator.AddMessageModule<ParamsModule>();

            cosmosNetworkConfigurator.AddApiModule<IBlocksApi, BlocksApi>();
            cosmosNetworkConfigurator.AddApiModule<ITransactionsApi, TransactionsApi>();
            cosmosNetworkConfigurator.AddApiModule<IWalletApi, WalletApi>();
            cosmosNetworkConfigurator.AddApiModule<IStakingApi, StakingApi>();
            cosmosNetworkConfigurator.AddApiModule<IGovApi, GovApi>();

            if (isDefaultClient)
            {
                services.AddKeyedSingleton<CosmosGenesis>(CosmosNetworkConfigurator.DEFAULT_KEY);
                services.AddKeyedSingleton(CosmosNetworkConfigurator.DEFAULT_KEY, cosmosNetworkConfigurator);
                services.AddKeyedSingleton(CosmosNetworkConfigurator.DEFAULT_KEY, options);

                services.AddSingleton(sp => sp.GetRequiredKeyedService<CosmosGenesis>(CosmosNetworkConfigurator.DEFAULT_KEY));
            }
            else
            {
                services.AddKeyedSingleton<CosmosGenesis>(clientName);
                services.AddKeyedSingleton(clientName, cosmosNetworkConfigurator);
                services.AddKeyedSingleton(clientName, options);
            }

            return cosmosNetworkConfigurator;
        }

        public static void SetupChain(this CosmosNetworkConfigurator configurator, NetworkOptions networkOptions)
        {
            configurator.SetupChain<CosmosApi>(networkOptions);
        }

        public static void SetupChain<T>(this CosmosNetworkConfigurator configurator, NetworkOptions networkOptions)
            where T : CosmosApi
        {
            configurator.SetupChain<T>(networkOptions);
        }

        public static void UseCosmosNetwork(this IServiceProvider provider)
        {
            UseCosmosNetwork(provider, CosmosNetworkConfigurator.DEFAULT_KEY);
        }

        public static void UseCosmosNetwork(this IServiceProvider provider, string clientName)
        {
            CosmosNetworkConfigurator configurator = provider.GetRequiredKeyedService<CosmosNetworkConfigurator>(clientName);

            foreach (Type moduleType in configurator.ConfiguredModules)
            {
                ICosmosMessageModule module = (ICosmosMessageModule)provider.GetRequiredKeyedService(moduleType, clientName);
                module.ConfigureModule(configurator.Options, configurator.Registry);
            }
        }
    }
}
