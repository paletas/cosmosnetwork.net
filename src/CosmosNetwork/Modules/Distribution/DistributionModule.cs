using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Distribution
{
    public class DistributionModule : ICosmosModule
    {
        public void ConfigureModule(CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageFundCommunityPool, Serialization.MessageFundCommunityPool>();
            messageRegistry.RegisterMessage<MessageSetWithdrawAddress, Serialization.MessageSetWithdrawAddress>();
            messageRegistry.RegisterMessage<MessageWithdrawDelegatorReward, Serialization.MessageWithdrawDelegatorReward>();
            messageRegistry.RegisterMessage<MessageWithdrawValidatorCommission, Serialization.MessageWithdrawValidatorCommission>();
        }
    }
}
