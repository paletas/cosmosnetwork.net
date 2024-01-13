using CosmosNetwork.API;
using CosmosNetwork.Modules;
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
}
