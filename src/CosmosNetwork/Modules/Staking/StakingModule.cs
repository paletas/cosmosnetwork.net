using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Staking
{
    public class StakingModule : ICosmosModule
    {
        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageBeginRedelegate, Serialization.MessageBeginRedelegate>();
            messageRegistry.RegisterMessage<MessageCreateValidator, Serialization.MessageCreateValidator>();
            messageRegistry.RegisterMessage<MessageDelegate, Serialization.MessageDelegate>();
            messageRegistry.RegisterMessage<MessageEditValidator, Serialization.MessageEditValidator>();
            messageRegistry.RegisterMessage<MessageUndelegate, Serialization.MessageUndelegate>();
        }
    }
}
