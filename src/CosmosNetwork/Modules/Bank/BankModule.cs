using CosmosNetwork.Modules.Authz;
using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using CosmosNetwork.Modules.Bank.Authz;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Bank
{
    public class BankModule : ICosmosModule
    {
        private readonly AuthzModule _authzModule;

        public BankModule(AuthzModule authzModule)
        {
            this._authzModule = authzModule;
        }

        public void ConfigureModule(CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageSend>();
            messageRegistry.RegisterMessage<MessageMultiSend>();

            this._authzModule.AuthorizationsRegistry.Register<Serialization.Authz.SendAuthorization>(SendAuthorization.AuthorizationType);
        }
    }
}
