﻿using CosmosNetwork.Modules.Authz;
using CosmosNetwork.Modules.Bank.Authz;

namespace CosmosNetwork.Modules.Bank
{
    public class BankModule : ICosmosMessageModule
    {
        private readonly AuthzModule _authzModule;

        public BankModule(AuthzModule authzModule)
        {
            this._authzModule = authzModule;
        }

        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageSend, Serialization.MessageSend>();
            messageRegistry.RegisterMessage<MessageMultiSend, Serialization.MessageMultiSend>();

            this._authzModule.AuthorizationsRegistry.Register<Serialization.Authz.SendAuthorization>(SendAuthorization.AuthorizationType);
        }
    }
}
