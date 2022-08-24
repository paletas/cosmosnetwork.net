using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Authz
{
    public class AuthzModule : ICosmosModule
    {
        private readonly IServiceCollection _services;

        public AuthzModule(IServiceCollection services)
        {
            this._services = services;

            this.AuthorizationsRegistry = new AuthorizationRegistry();
        }

        public AuthorizationRegistry AuthorizationsRegistry { get; init; }

        public void ConfigureModule(CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageExecute>();
            messageRegistry.RegisterMessage<MessageGrant>();
            messageRegistry.RegisterMessage<MessageRevoke>();

            _services.AddSingleton(this.AuthorizationsRegistry);
        }
    }
}
