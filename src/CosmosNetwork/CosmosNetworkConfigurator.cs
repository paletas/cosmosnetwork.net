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
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProtoBuf.Meta;
using UltimateOrb;

namespace CosmosNetwork
{
    public class CosmosNetworkConfigurator
    {
        internal CosmosNetworkConfigurator(IServiceCollection services, string endpoint, CosmosMessageRegistry registry, CosmosApiOptions options)
        {
            this.Services = services;
            this.Options = options;
            this.Registry = registry;
            this.Endpoint = endpoint;
        }

        public CosmosApiOptions Options { get; }

        public NetworkOptions? Network { get; set; }

        public CosmosMessageRegistry Registry { get; }

        public IServiceCollection Services { get; }

        public string Endpoint { get; }

        public CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment)
        {
            this.Options.GasAdjustment = gasAdjustment;
            this.Options.GasPrices = null;

            return this;
        }

        public CosmosNetworkConfigurator SetGasOptions(decimal gasAdjustment, params CoinDecimal[] gasPrices)
        {
            this.Options.GasAdjustment = gasAdjustment;
            this.Options.GasPrices = gasPrices;

            return this;
        }

        public CosmosNetworkConfigurator AddMessageModule<T>()
            where T : class, ICosmosMessageModule
        {
            _ = this.Services.AddSingleton<T>();
            _ = this.Services.AddSingleton<ICosmosMessageModule>(sp => sp.GetRequiredService<T>());

            return this;
        }

        public CosmosNetworkConfigurator AddMessageModule<T>(T module)
            where T : class, ICosmosMessageModule
        {
            _ = this.Services.AddSingleton<T>(module);
            _ = this.Services.AddSingleton<ICosmosMessageModule>(module);

            return this;
        }

        public CosmosNetworkConfigurator ReplaceMessageModule<TE, TN>(TN newModule)
            where TE : class, ICosmosMessageModule
            where TN : class, ICosmosMessageModule
        {
            _ = this.Services.RemoveAll<TE>();

            _ = this.Services.AddSingleton<TN>(newModule);
            _ = this.Services.AddSingleton<ICosmosMessageModule>(newModule);

            return this;
        }

        public CosmosNetworkConfigurator RemoveMessageModule<T>()
            where T : class, ICosmosMessageModule
        {
            _ = this.Services.RemoveAll<T>();

            return this;
        }

        public CosmosNetworkConfigurator AddApiModule<T>()
            where T : CosmosApiModule
        {
            _ = this.Services.AddScoped<T>();

            return this;
        }

        public CosmosNetworkConfigurator AddApiModule<TI, TP>()
            where TI : class
            where TP : CosmosApiModule, TI
        {
            this.Services.AddScoped<TI, TP>();
            return this;
        }

        public CosmosNetworkConfigurator ReplaceApiModule<TI, TP>()
            where TI : class
            where TP : CosmosApiModule, TI
        {
            _ = this.Services.RemoveAll<TI>();
            _ = this.Services.AddScoped<TI, TP>();

            return this;
        }
    } 

    public static class CosmosNetworkConfiguration
    {
        public static CosmosNetworkConfigurator AddCosmosNetwork(this IServiceCollection services, string endpoint, CosmosApiOptions? options = null)
        {
            const string DEFAULT_HTTPCLIENT = "HTTP_Default";

            CosmosMessageRegistry cosmosMessageRegistry = new();
            options ??= new CosmosApiOptions(DEFAULT_HTTPCLIENT);
            options.MessageRegistry = cosmosMessageRegistry;

            if ((options.HttpClientName ?? DEFAULT_HTTPCLIENT) == DEFAULT_HTTPCLIENT)
            {
                services.AddHttpClient(DEFAULT_HTTPCLIENT, client => client.BaseAddress = new Uri(endpoint));
            }     

            CosmosNetworkConfigurator cosmosNetworkConfigurator = new(services, endpoint, cosmosMessageRegistry, options);

            cosmosNetworkConfigurator.AddMessageModule<AuthzModule>();
            cosmosNetworkConfigurator.AddMessageModule<GovModule>();
            cosmosNetworkConfigurator.AddMessageModule<BankModule>();
            cosmosNetworkConfigurator.AddMessageModule<DistributionModule>();
            cosmosNetworkConfigurator.AddMessageModule<FeeGrantModule>();
            cosmosNetworkConfigurator.AddMessageModule<SlashingModule>();
            cosmosNetworkConfigurator.AddMessageModule<StakingModule>();

            cosmosNetworkConfigurator.AddApiModule<IBlocksApi, BlocksApi>();
            cosmosNetworkConfigurator.AddApiModule<ITransactionsApi, TransactionsApi>();
            cosmosNetworkConfigurator.AddApiModule<IWalletApi, WalletApi>();
            cosmosNetworkConfigurator.AddApiModule<IStakingApi, StakingApi>();

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
            configurator.Services.AddScoped<T>();
            configurator.Services.AddSingleton(networkOptions);
        }

        public static void UseCosmosNetwork(this IApplicationBuilder application)
        {
            application.ApplicationServices.UseCosmosNetwork();
        }

        public static void UseCosmosNetwork(this IServiceProvider provider)
        {
            CosmosNetworkConfigurator configurator = provider.GetRequiredService<CosmosNetworkConfigurator>();
            IEnumerable<ICosmosMessageModule> modules = provider.GetServices<ICosmosMessageModule>();
            foreach (ICosmosMessageModule module in modules)
            {
                module.ConfigureModule(configurator.Options, configurator.Registry);
            }
        }
    }
}
