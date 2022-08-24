using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Staking
{
    public class StakingModule : ICosmosModule
    {
        public void ConfigureModule(CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageBeginRedelegate>();
            messageRegistry.RegisterMessage<MessageCreateValidator>();
            messageRegistry.RegisterMessage<MessageDelegate>();
            messageRegistry.RegisterMessage<MessageEditValidator>();
            messageRegistry.RegisterMessage<MessageUndelegate>();
            messageRegistry.RegisterMessage<MessageBeginRedelegate>();
        }
    }
}
