using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Authz
{
    internal static class AuthzConfiguration
    {
        public static IServiceCollection ConfigureModule(this IServiceCollection serviceCollection, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageExecute>();
            messageRegistry.RegisterMessage<MessageGrant>();
            messageRegistry.RegisterMessage<MessageRevoke>();

            AuthorizationRegistry authorizationRegistry = new();
            _ = serviceCollection.AddSingleton(authorizationRegistry);

            return serviceCollection;
        }
    }
}
