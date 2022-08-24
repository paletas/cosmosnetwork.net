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

namespace CosmosNetwork
{
    public class CosmosNetworkConfigurator
    {
        private readonly IServiceCollection _services;
        private readonly CosmosApiOptions _options;

        internal CosmosNetworkConfigurator(IServiceCollection services, string chainId, CosmosMessageRegistry registry, CosmosApiOptions? options = null)
        {
            _options = options ?? new CosmosApiOptions(chainId);
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

        public CosmosNetworkConfigurator AddModule<T>()
            where T : class, ICosmosModule
        {
            _services.AddSingleton<T>();
            _services.AddSingleton<ICosmosModule, T>(sp => sp.GetRequiredService<T>());

            return this;
        }

        public CosmosNetworkConfigurator ReplaceModule<TE, TN>()
            where TE : class, ICosmosModule
            where TN : class, ICosmosModule
        {
            _services.RemoveAll<TE>();

            _services.AddSingleton<TN>();
            _services.AddSingleton<ICosmosModule, TN>(sp => sp.GetRequiredService<TN>());

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
        public static CosmosNetworkConfigurator AddCosmosNetwork(this IServiceCollection services, string chainId, CosmosApiOptions? options = null)
        {
            CosmosMessageRegistry cosmosMessageRegistry = new();

            CosmosNetworkConfigurator cosmosNetworkConfigurator = new(services, chainId, cosmosMessageRegistry, options);

            cosmosNetworkConfigurator.AddModule<AuthzModule>();
            cosmosNetworkConfigurator.AddModule<BankModule>();
            cosmosNetworkConfigurator.AddModule<DistributionModule>();
            cosmosNetworkConfigurator.AddModule<FeeGrantModule>();
            cosmosNetworkConfigurator.AddModule<GovModule>();
            cosmosNetworkConfigurator.AddModule<SlashingModule>();
            cosmosNetworkConfigurator.AddModule<StakingModule>();

            services.AddSingleton(sp => cosmosMessageRegistry);
            services.AddScoped<CosmosApi>();

            return cosmosNetworkConfigurator;
        }

        public static void UseCosmosNetwork(this IServiceProvider provider)
        {
            var modules = provider.GetServices<ICosmosModule>();
            foreach (var module in modules)
            {
                module.ConfigureModule(provider.GetRequiredService<CosmosMessageRegistry>());
            }
        }
    }
}
