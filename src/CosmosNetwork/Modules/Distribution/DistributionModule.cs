using CosmosNetwork.Modules.Authz;
using CosmosNetwork.Modules.Distribution.Serialization.Proposals;
using CosmosNetwork.Modules.Gov;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Distribution
{
    public class DistributionModule : ICosmosMessageModule
    {
        private readonly GovModule _governanceModule;

        public DistributionModule(GovModule governanceModule)
        {
            this._governanceModule = governanceModule;
        }

        public DistributionModule([ServiceKey] string serviceKey, IServiceProvider serviceProvider)
        {
            this._governanceModule = serviceProvider.GetRequiredKeyedService<GovModule>(serviceKey);
        }

        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageFundCommunityPool, Serialization.MessageFundCommunityPool>(MessageFundCommunityPool.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageSetWithdrawAddress, Serialization.MessageSetWithdrawAddress>(MessageSetWithdrawAddress.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageWithdrawDelegatorReward, Serialization.MessageWithdrawDelegatorReward>(MessageWithdrawDelegatorReward.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageWithdrawValidatorCommission, Serialization.MessageWithdrawValidatorCommission>(MessageWithdrawValidatorCommission.COSMOS_DESCRIPTOR);

            if (cosmosOptions.RegisterBetaMessages)
            {
                messageRegistry.RegisterMessage<MessageFundCommunityPool, Serialization.MessageFundCommunityPool>(MessageFundCommunityPool.COSMOS_BETA_DESCRIPTOR);
                messageRegistry.RegisterMessage<MessageSetWithdrawAddress, Serialization.MessageSetWithdrawAddress>(MessageSetWithdrawAddress.COSMOS_BETA_DESCRIPTOR);
                messageRegistry.RegisterMessage<MessageWithdrawDelegatorReward, Serialization.MessageWithdrawDelegatorReward>(MessageWithdrawDelegatorReward.COSMOS_BETA_DESCRIPTOR);
                messageRegistry.RegisterMessage<MessageWithdrawValidatorCommission, Serialization.MessageWithdrawValidatorCommission>(MessageWithdrawValidatorCommission.COSMOS_BETA_DESCRIPTOR);
            }

            this._governanceModule.ProposalsRegistry.Register<CommunityPoolSpendProposal>(CommunityPoolSpendProposal.ProposalType);
        }
    }
}
