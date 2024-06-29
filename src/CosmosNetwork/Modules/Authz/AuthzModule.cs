using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using CosmosNetwork.Modules.Authz.Serialization.Json;

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
            cosmosOptions.JsonSerializerOptions.Converters.Add(new AuthorizationConverter(this.AuthorizationsRegistry));
            cosmosOptions.JsonSerializerOptions.Converters.Add(new AuthorizationsConverter(this.AuthorizationsRegistry));

            messageRegistry.RegisterMessage<MessageExecute, Serialization.MessageExecute>();
            messageRegistry.RegisterMessage<MessageGrant, Serialization.MessageGrant>();
            messageRegistry.RegisterMessage<MessageRevoke, Serialization.MessageRevoke>();
        }
    }
}
