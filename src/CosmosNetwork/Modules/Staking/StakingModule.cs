using CosmosNetwork.Modules.Staking.Messages;

namespace CosmosNetwork.Modules.Staking
{
    public class StakingModule : ICosmosModule
    {
        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageBeginRedelegate, Messages.Serialization.MessageBeginRedelegate>();
            messageRegistry.RegisterMessage<MessageCreateValidator, Messages.Serialization.MessageCreateValidator>();
            messageRegistry.RegisterMessage<MessageDelegate, Messages.Serialization.MessageDelegate>();
            messageRegistry.RegisterMessage<MessageEditValidator, Messages.Serialization.MessageEditValidator>();
            messageRegistry.RegisterMessage<MessageUndelegate, Messages.Serialization.MessageUndelegate>();
        }
    }
}
