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
            messageRegistry.RegisterMessage<MessageFundCommunityPool, Serialization.MessageFundCommunityPool>();
            messageRegistry.RegisterMessage<MessageSetWithdrawAddress, Serialization.MessageSetWithdrawAddress>();
            messageRegistry.RegisterMessage<MessageWithdrawDelegatorReward, Serialization.MessageWithdrawDelegatorReward>();
            messageRegistry.RegisterMessage<MessageWithdrawValidatorCommission, Serialization.MessageWithdrawValidatorCommission>();

            this._governanceModule.ProposalsRegistry.Register<CommunityPoolSpendProposal>(CommunityPoolSpendProposal.ProposalType);
        }
    }
}
