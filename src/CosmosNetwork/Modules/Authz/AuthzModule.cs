﻿using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
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
            messageRegistry.RegisterMessage<MessageExecute, Serialization.MessageExecute>();
            messageRegistry.RegisterMessage<MessageGrant, Serialization.MessageGrant>();
            messageRegistry.RegisterMessage<MessageRevoke, Serialization.MessageRevoke>();

            _services.AddSingleton(this.AuthorizationsRegistry);
        }
    }
}
