using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Authz
{
    public class AuthzModule : ICosmosMessageModule
    {
        public AuthzModule()
        {
            this.AuthorizationsRegistry = new AuthorizationRegistry();
        }

        public AuthorizationRegistry AuthorizationsRegistry { get; init; }

        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageExecute, Serialization.MessageExecute>();
            messageRegistry.RegisterMessage<MessageGrant, Serialization.MessageGrant>();
            messageRegistry.RegisterMessage<MessageRevoke, Serialization.MessageRevoke>();
        }
    }
}
