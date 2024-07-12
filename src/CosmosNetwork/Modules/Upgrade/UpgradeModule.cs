using CosmosNetwork.Modules.Gov;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Upgrade
{
    public class UpgradeModule : ICosmosMessageModule
    {
        private readonly GovModule _governanceModule;

        public UpgradeModule(GovModule governanceModule)
        {
            this._governanceModule = governanceModule;
        }

        public UpgradeModule([ServiceKey] string serviceKey, IServiceProvider serviceProvider)
        {
            this._governanceModule = serviceProvider.GetRequiredKeyedService<GovModule>(serviceKey);
        }


        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            this._governanceModule.ProposalsRegistry.Register<Serialization.Proposals.SoftwareUpgradeProposal>(Serialization.Proposals.SoftwareUpgradeProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<Serialization.Proposals.CancelSoftwareUpgradeProposal>(Serialization.Proposals.CancelSoftwareUpgradeProposal.ProposalType);
        }
    }
}
