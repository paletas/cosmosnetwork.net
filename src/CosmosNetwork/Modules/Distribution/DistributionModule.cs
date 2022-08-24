using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Distribution
{
    public class DistributionModule : ICosmosModule
    {
        public void ConfigureModule(CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageFundCommunityPool>();
            messageRegistry.RegisterMessage<MessageSetWithdrawAddress>();
            messageRegistry.RegisterMessage<MessageWithdrawDelegatorReward>();
            messageRegistry.RegisterMessage<MessageWithdrawValidatorCommission>();
        }
    }
}
