using CosmosNetwork.Modules;
using CosmosNetwork.Modules.Authz;
using CosmosNetwork.Modules.Bank;
using CosmosNetwork.Modules.Distribution;
using CosmosNetwork.Modules.FeeGrant;
using CosmosNetwork.Modules.Gov;
using CosmosNetwork.Modules.Slashing;
using CosmosNetwork.Modules.Staking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace CosmosNetwork
{
    public class CosmosNetworkConfigurator
    {
        private readonly IServiceCollection _services;
        private readonly CosmosApiOptions _options;

        internal CosmosNetworkConfigurator(IServiceCollection services, string chainId, CosmosMessageRegistry registry, CosmosApiOptions options)
        {
            _options = options;
            _services = services;
            Registry = registry;
        }

        public CosmosMessageRegistry Registry { get; }

        public CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment)
        {
            _options.GasAdjustment = gasAdjustment;
            _options.GasPrices = null;

            return this;
        }

        public CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment, params CoinDecimal[] gasPrices)
        {
            _options.GasAdjustment = gasAdjustment;
            _options.GasPrices = gasPrices;

            return this;
        }

        public CosmosNetworkConfigurator SetDefaultDenom(params string[] denom)
        {
            _options.DefaultDenoms = denom;

            return this;
        }

        public CosmosNetworkConfigurator AddModule<T>(T module)
            where T : class, ICosmosModule
        {
            _services.AddSingleton<T>(module);
            _services.AddSingleton<ICosmosModule>(module);

            return this;
        }

        public CosmosNetworkConfigurator ReplaceModule<TE, TN>(TN newModule)
            where TE : class, ICosmosModule
            where TN : class, ICosmosModule
        {
            _services.RemoveAll<TE>();

            _services.AddSingleton<TN>(newModule);
            _services.AddSingleton<ICosmosModule>(newModule);

            return this;
        }

        public CosmosNetworkConfigurator RemoveModule<T>()
            where T : class, ICosmosModule
        {
            _services.RemoveAll<T>();

            return this;
        }
    }

    public static class CosmosNetworkConfiguration
    {
        public static CosmosNetworkConfigurator AddCosmosNetwork(this IServiceCollection services, string chainId, string endpoint, CosmosApiOptions? options = null)
        {
            CosmosMessageRegistry cosmosMessageRegistry = new();

            CosmosNetworkConfigurator cosmosNetworkConfigurator = new(services, chainId, cosmosMessageRegistry, options);

            options ??= new CosmosApiOptions();
            options.ChainId = chainId;
            options.MessageRegistry = cosmosMessageRegistry;

            AuthzModule authzModule;

            cosmosNetworkConfigurator.AddModule(authzModule = new AuthzModule(services));
            cosmosNetworkConfigurator.AddModule(new BankModule(authzModule));
            cosmosNetworkConfigurator.AddModule(new DistributionModule());
            cosmosNetworkConfigurator.AddModule(new FeeGrantModule(services));
            cosmosNetworkConfigurator.AddModule(new GovModule(services));
            cosmosNetworkConfigurator.AddModule(new SlashingModule());
            cosmosNetworkConfigurator.AddModule(new StakingModule());

            services.AddSingleton(sp => cosmosMessageRegistry);
            services.AddSingleton(sp => new CosmosApi(new HttpClient() { BaseAddress = new Uri(endpoint) }, sp.GetRequiredService<ILoggerFactory>(), options));

            return cosmosNetworkConfigurator;
        }

        public static void UseCosmosNetwork(this IServiceProvider provider, string? endpoint = null)
        {
            var modules = provider.GetServices<ICosmosModule>();
            foreach (var module in modules)
            {
                module.ConfigureModule(provider.GetRequiredService<CosmosMessageRegistry>());
            }
        }
    }
}
