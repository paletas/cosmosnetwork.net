using CosmosNetwork.Modules.Staking.Messages;

namespace CosmosNetwork.Modules.Staking
{
    public class StakingModule : ICosmosMessageModule
    {
        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageBeginRedelegate, Messages.Serialization.MessageBeginRedelegate>(MessageBeginRedelegate.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageCreateValidator, Messages.Serialization.MessageCreateValidator>(MessageCreateValidator.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageDelegate, Messages.Serialization.MessageDelegate>(MessageDelegate.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageEditValidator, Messages.Serialization.MessageEditValidator>(MessageEditValidator.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageUndelegate, Messages.Serialization.MessageUndelegate>(MessageUndelegate.COSMOS_DESCRIPTOR);
        }
    }
}
