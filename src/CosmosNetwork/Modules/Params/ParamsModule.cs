using CosmosNetwork.Modules.Distribution.Serialization.Proposals;
using CosmosNetwork.Modules.Gov;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Params
{
    internal class ParamsModule : ICosmosMessageModule
    {
        private readonly GovModule _govModule;

        public ParamsModule(GovModule govModule)
        {
            this._govModule = govModule;
        }

        public ParamsModule([ServiceKey] string serviceKey, IServiceProvider serviceProvider)
        {
            this._govModule = serviceProvider.GetRequiredKeyedService<GovModule>(serviceKey);
        }

        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            this._govModule.ProposalsRegistry.Register<CommunityPoolSpendProposal>(CommunityPoolSpendProposal.ProposalType);
        }
    }
}
