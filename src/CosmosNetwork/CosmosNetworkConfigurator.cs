using CosmosNetwork.API;
using CosmosNetwork.API.Impl;
using CosmosNetwork.Modules;
using CosmosNetwork.Modules.Authz;
using CosmosNetwork.Modules.Bank;
using CosmosNetwork.Modules.Distribution;
using CosmosNetwork.Modules.FeeGrant;
using CosmosNetwork.Modules.Gov;
using CosmosNetwork.Modules.Slashing;
using CosmosNetwork.Modules.Staking;
using CosmosNetwork.Serialization.Proto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProtoBuf.Meta;
using UltimateOrb;

namespace CosmosNetwork
{
    public class CosmosNetworkConfigurator
    {
        internal CosmosNetworkConfigurator(IServiceCollection services, string endpoint, CosmosMessageRegistry registry, CosmosApiOptions options)
        {
            Services = services;
            Options = options;
            Registry = registry;
            Endpoint = endpoint;
        }

        public CosmosApiOptions Options { get; }

        public NetworkOptions? Network { get; set; }

        public CosmosMessageRegistry Registry { get; }

        public IServiceCollection Services { get; }

        public string Endpoint { get; }

        public CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment)
        {
            Options.GasAdjustment = gasAdjustment;
            Options.GasPrices = null;

            return this;
        }

        public CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment, params CoinDecimal[] gasPrices)
        {
            Options.GasAdjustment = gasAdjustment;
            Options.GasPrices = gasPrices;

            return this;
        }

        public CosmosNetworkConfigurator SetDefaultDenom(params string[] denom)
        {
            Options.DefaultDenoms = denom;

            return this;
        }

        public CosmosNetworkConfigurator AddMessageModule<T>()
            where T : class, ICosmosMessageModule
        {
            Services.AddSingleton<T>();
            Services.AddSingleton<ICosmosMessageModule>(sp => sp.GetRequiredService<T>());

            return this;
        }

        public CosmosNetworkConfigurator AddMessageModule<T>(T module)
            where T : class, ICosmosMessageModule
        {
            Services.AddSingleton<T>(module);
            Services.AddSingleton<ICosmosMessageModule>(module);

            return this;
        }

        public CosmosNetworkConfigurator ReplaceMessageModule<TE, TN>(TN newModule)
            where TE : class, ICosmosMessageModule
            where TN : class, ICosmosMessageModule
        {
            Services.RemoveAll<TE>();

            Services.AddSingleton<TN>(newModule);
            Services.AddSingleton<ICosmosMessageModule>(newModule);

            return this;
        }

        public CosmosNetworkConfigurator RemoveMessageModule<T>()
            where T : class, ICosmosMessageModule
        {
            Services.RemoveAll<T>();

            return this;
        }

        public CosmosNetworkConfigurator AddApiModule<T>()
            where T : CosmosApiModule
        {
            Services.AddHttpClient<T>();

            return this;
        }

        public CosmosNetworkConfigurator AddApiModule<TI, TP>(string basePath)
            where TI : class
            where TP : CosmosApiModule, TI
        {
            Services.AddHttpClient<TI, TP>(client => client.BaseAddress = new Uri(basePath, UriKind.Absolute));

            return this;
        }

        public CosmosNetworkConfigurator ReplaceApiModule<TI, TP>(string basePath)
            where TI : class
            where TP : CosmosApiModule, TI
        {
            Services.RemoveAll<TI>();
            Services.AddHttpClient<TI, TP>(client => client.BaseAddress = new Uri(basePath, UriKind.Absolute));

            return this;
        }
    }

    public static class CosmosNetworkConfiguration
    {
        public static CosmosNetworkConfigurator AddCosmosNetwork(this IServiceCollection services, string endpoint, CosmosApiOptions? options = null)
        {
            CosmosMessageRegistry cosmosMessageRegistry = new();
            options ??= new CosmosApiOptions();
            options.MessageRegistry = cosmosMessageRegistry;

            CosmosNetworkConfigurator cosmosNetworkConfigurator = new(services, endpoint, cosmosMessageRegistry, options);

            AuthzModule authzModule;
            GovModule govModule;

            cosmosNetworkConfigurator.AddMessageModule(authzModule = new AuthzModule(services));
            cosmosNetworkConfigurator.AddMessageModule(govModule = new GovModule(services));
            cosmosNetworkConfigurator.AddMessageModule(new BankModule(authzModule));
            cosmosNetworkConfigurator.AddMessageModule(new DistributionModule(govModule));
            cosmosNetworkConfigurator.AddMessageModule(new FeeGrantModule(services));
            cosmosNetworkConfigurator.AddMessageModule(new SlashingModule());
            cosmosNetworkConfigurator.AddMessageModule(new StakingModule());

            cosmosNetworkConfigurator.AddApiModule<IBlocksApi, BlocksApi>(endpoint);
            cosmosNetworkConfigurator.AddApiModule<ITransactionsApi, TransactionsApi>(endpoint);
            cosmosNetworkConfigurator.AddApiModule<IWalletApi, WalletApi>(endpoint);
            cosmosNetworkConfigurator.AddApiModule<IStakingApi, StakingApi>(endpoint);

            services.AddSingleton(cosmosNetworkConfigurator);
            services.AddSingleton(options);

            if (RuntimeTypeModel.Default.IsDefined(typeof(UInt128)) == false)
            {
                RuntimeTypeModel.Default.Add<UInt128>()
                    .SetSurrogate(typeof(ProtoUInt128));
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
            configurator.Network = networkOptions;
            configurator.Services.AddTransient<T>();
            configurator.Services.AddSingleton(networkOptions);
        }

        public static void UseCosmosNetwork(this IServiceProvider provider)
        {
            var configurator = provider.GetRequiredService<CosmosNetworkConfigurator>();
            var modules = provider.GetServices<ICosmosMessageModule>();
            foreach (var module in modules)
            {
                module.ConfigureModule(configurator.Options, configurator.Registry);
            }
        }
    }
}
