using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork
{
    public class CosmosNetworkConfigurator
    {
        private readonly CosmosApiOptions _options;

        internal CosmosNetworkConfigurator(string chainId, CosmosMessageRegistry registry, CosmosApiOptions? options = null)
        {
            _options = options ?? new CosmosApiOptions(chainId);

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
    }

    public static class CosmosNetworkConfiguration
    {
        public static CosmosNetworkConfigurator UseCosmosNetwork(this IServiceCollection services, string chainId, CosmosApiOptions? options = null)
        {
            CosmosMessageRegistry cosmosMessageRegistry = new();
            cosmosMessageRegistry.RegisterMessages(typeof(CosmosNetworkConfigurator).Assembly);

            CosmosNetworkConfigurator cosmosNetworkConfigurator = new(chainId, cosmosMessageRegistry, options);

            _ = services.AddSingleton<CosmosNetworkConfigurator>(sp => cosmosNetworkConfigurator);
            _ = services.AddSingleton<CosmosMessageRegistry>(sp => cosmosMessageRegistry);
            _ = services.AddScoped<CosmosApi>();

            return cosmosNetworkConfigurator;
        }
    }
}
