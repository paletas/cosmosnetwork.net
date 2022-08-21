using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Distribution
{
    internal static class DistributionConfiguration
    {
        public static IServiceCollection ConfigureModule(this IServiceCollection serviceCollection, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageFundCommunityPool>();
            messageRegistry.RegisterMessage<MessageSetWithdrawAddress>();
            messageRegistry.RegisterMessage<MessageWithdrawDelegatorReward>();
            messageRegistry.RegisterMessage<MessageWithdrawValidatorCommission>();

            return serviceCollection;
        }
    }
}
