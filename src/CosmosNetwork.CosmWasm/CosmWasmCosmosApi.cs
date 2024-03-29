using CosmosNetwork.CosmWasm.API;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork
{
    public partial class CosmWasmCosmosApi([ServiceKey] string serviceKey, IServiceProvider serviceProvider)
        : CosmosApi(serviceKey, serviceProvider)
    {
        public ICosmWasmApi CosmWasm { get; init; } = serviceProvider.GetRequiredKeyedService<ICosmWasmApi>(serviceKey);
    }
}
