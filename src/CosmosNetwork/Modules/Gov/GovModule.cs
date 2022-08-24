using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Gov
{
    public class GovModule : ICosmosModule
    {
        private readonly IServiceCollection _services;

        public GovModule(IServiceCollection services)
        {
            this._services = services;

            this.ProposalsRegistry = new ProposalsRegistry();
        }

        public ProposalsRegistry ProposalsRegistry { get; init; }

        public void ConfigureModule(CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageDeposit>();
            messageRegistry.RegisterMessage<MessageSubmitProposal>();
            messageRegistry.RegisterMessage<MessageVote>();
            messageRegistry.RegisterMessage<MessageVoteWeighted>();

            _services.AddSingleton(this.ProposalsRegistry);

            this.ProposalsRegistry.Register<Serialization.Proposals.TextProposal>(Gov.Proposals.TextProposal.ProposalType);
        }
    }
}
