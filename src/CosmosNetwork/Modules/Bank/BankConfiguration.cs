using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using CosmosNetwork.Modules.Bank.Authz;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Bank
{
    internal static class BankConfiguration
    {
        public static IServiceCollection ConfigureModule(this IServiceCollection serviceCollection, CosmosMessageRegistry messageRegistry, AuthorizationRegistry authorizationRegistry)
        {
            messageRegistry.RegisterMessage<MessageSend>();
            messageRegistry.RegisterMessage<MessageMultiSend>();

            authorizationRegistry.Register<Serialization.Authz.SendAuthorization>(SendAuthorization.AuthorizationType);

            return serviceCollection;
        }
    }
}
