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
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CosmosNetwork
{
    public class CosmosNetworkConfigurator
    {
        internal const string DEFAULT_KEY = "DEFAULT";

        private readonly string _serviceKey;
        private readonly List<Type> _configuredModuleTypes = [];

        protected internal CosmosNetworkConfigurator(IServiceCollection services, CosmosMessageRegistry registry, CosmosApiOptions options, bool useKeyedInstances, string? serviceKey = null)
        {
            this._serviceKey = useKeyedInstances ? serviceKey! : DEFAULT_KEY;

            this.Services = services;
            this.Options = options;
            this.Registry = registry;
        }

        public virtual CosmosApiOptions Options { get; }

        public virtual CosmosMessageRegistry Registry { get; }

        public virtual IServiceCollection Services { get; }

        public virtual NetworkOptions? Network { get; set; }

        internal IEnumerable<Type> ConfiguredModules => this._configuredModuleTypes;

        public virtual CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment)
        {
            this.Options.GasAdjustment = gasAdjustment;
            this.Options.GasPrices = null;

            return this;
        }

        public virtual CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment, params CoinDecimal[] gasPrices)
        {
            this.Options.GasAdjustment = gasAdjustment;
            this.Options.GasPrices = gasPrices;

            return this;
        }

        public CosmosNetworkConfigurator AddMessageModule<T>()
            where T : class, ICosmosMessageModule
        {
            this._configuredModuleTypes.Add(typeof(T));

            this.Services.AddKeyedSingleton<T>(this._serviceKey);
            this.Services.AddKeyedSingleton<ICosmosMessageModule>(this._serviceKey, (sp, key) => sp.GetRequiredKeyedService<T>(key));

            return this;
        }

        public CosmosNetworkConfigurator AddMessageModule<T>(T module)
            where T : class, ICosmosMessageModule
        {
            this._configuredModuleTypes.Add(typeof(T));

            this.Services.AddKeyedSingleton<T>(this._serviceKey, module);
            this.Services.AddKeyedSingleton<ICosmosMessageModule>(this._serviceKey, module);

            return this;
        }

        public CosmosNetworkConfigurator ReplaceMessageModule<TE, TN>(TN newModule)
            where TE : class, ICosmosMessageModule
            where TN : class, ICosmosMessageModule
        {
            this._configuredModuleTypes.Remove(typeof(TE));
            this._configuredModuleTypes.Add(typeof(TN));

            this.Services.RemoveAllKeyed<TE>(this._serviceKey);

            this.Services.AddKeyedSingleton<TN>(this._serviceKey, newModule);
            this.Services.AddKeyedSingleton<ICosmosMessageModule>(this._serviceKey, (sp, key) => newModule);

            return this;
        }

        public CosmosNetworkConfigurator RemoveMessageModule<T>()
            where T : class, ICosmosMessageModule
        {
            this._configuredModuleTypes.Remove(typeof(T));

            this.Services.RemoveAllKeyed<T>(this._serviceKey);

            return this;
        }

        public CosmosNetworkConfigurator AddApiModule<T>()
            where T : CosmosApiModule
        {
            this.Services.AddKeyedScoped<T>(this._serviceKey);

            return this;
        }

        public CosmosNetworkConfigurator AddApiModule<TI, TP>()
            where TI : class
            where TP : CosmosApiModule, TI
        {
            this.Services.AddKeyedScoped<TI, TP>(this._serviceKey);

            return this;
        }

        public CosmosNetworkConfigurator ReplaceApiModule<TI, TP>()
            where TI : class
            where TP : CosmosApiModule, TI
        {
            this.Services.RemoveAllKeyed<TI>(this._serviceKey);
            this.Services.AddKeyedScoped<TI, TP>(this._serviceKey);

            return this;
        }

        internal void SetupChain<T>(NetworkOptions networkOptions)
            where T : CosmosApi
        {
            this.Network = networkOptions;

            if (this._serviceKey == DEFAULT_KEY)
            {
                this.Services.AddScoped(sp => sp.GetRequiredKeyedService<T>(DEFAULT_KEY));
            }

            this.Services.AddKeyedScoped<T>(this._serviceKey);
            this.Services.AddKeyedSingleton(this._serviceKey, networkOptions);
        }
    }

    public static class CosmosNetworkConfiguration
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
                services.AddKeyedSingleton(CosmosNetworkConfigurator.DEFAULT_KEY, cosmosNetworkConfigurator);
                services.AddKeyedSingleton(CosmosNetworkConfigurator.DEFAULT_KEY, options);
            }
            else
            {
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
