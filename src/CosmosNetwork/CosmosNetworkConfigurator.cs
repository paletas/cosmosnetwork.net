using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork
{
    public class CosmosNetworkConfigurator
    {
        private readonly CosmosApiOptions _options;

        internal CosmosNetworkConfigurator(string chainId, CosmosMessageRegistry registry, CosmosApiOptions? options = null)
        {
            this._options = options ?? new CosmosApiOptions(chainId);

            this.Registry = registry;
        }

        public CosmosMessageRegistry Registry { get; }

        public CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment)
        {
            this._options.GasAdjustment = gasAdjustment;
            this._options.GasPrices = null;

            return this;
        }

        public CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment, params CoinDecimal[] gasPrices)
        {
            this._options.GasAdjustment = gasAdjustment;
            this._options.GasPrices = gasPrices;

            return this;
        }

        public CosmosNetworkConfigurator SetDefaultDenom(params string[] denom)
        {
            this._options.DefaultDenoms = denom;

            return this;
        }
    }

    public static class CosmosNetworkConfiguration
    {
        public static CosmosNetworkConfigurator UseCosmosNetwork(this IServiceCollection services, string chainId, CosmosApiOptions? options = null)
        {
            var cosmosMessageRegistry = new CosmosMessageRegistry();
            cosmosMessageRegistry.RegisterMessages(typeof(CosmosNetworkConfigurator).Assembly);

            var cosmosNetworkConfigurator = new CosmosNetworkConfigurator(chainId, cosmosMessageRegistry, options);
            
            services.AddSingleton<CosmosNetworkConfigurator>(sp => cosmosNetworkConfigurator);
            services.AddSingleton<CosmosMessageRegistry>(sp => cosmosMessageRegistry);
            services.AddScoped<CosmosApi>();

            return cosmosNetworkConfigurator;
        }
    }
}
