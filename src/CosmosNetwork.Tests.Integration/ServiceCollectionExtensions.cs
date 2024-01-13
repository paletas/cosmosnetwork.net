using CosmosNetwork.Tests.Integration.Modules;
using CosmosNetwork.Tests.Integration.Modules.API;
using CosmosNetwork.Tests.Integration.Modules.Genesis;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Tests.Integration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureIntegrationTests(this IServiceCollection services)
        {
            services.AddSingleton<ApiIntegrationTestLibrary>();

            services.AddSingleton<IModule, ApiModule>();
            services.AddSingleton<IModule, GenesisModule>();
        }
    }
}
