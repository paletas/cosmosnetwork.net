using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules
{
    public interface ICosmosModule
    {
        void ConfigureModule(CosmosMessageRegistry messageRegistry);
    }
}
