using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using CosmosNetwork.Modules.Gov.Serialization.Proposals.Json;
using CosmosNetwork.Modules.Params.Serialization.Proposals;

namespace CosmosNetwork.Modules.Gov
{
    public class GovModule : ICosmosMessageModule
    {
        public GovModule()
        {
            this.ProposalsRegistry = new ProposalsRegistry();
        }

        public ProposalsRegistry ProposalsRegistry { get; init; }

        public virtual void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageDeposit, Serialization.MessageDeposit>();
            messageRegistry.RegisterMessage<MessageSubmitProposal, Serialization.MessageSubmitProposal>();
            messageRegistry.RegisterMessage<MessageVote, Serialization.MessageVote>();
            messageRegistry.RegisterMessage<MessageVoteWeighted, Serialization.MessageVoteWeighted>();

            cosmosOptions.JsonSerializerOptions.Converters.Add(new ProposalConverter(this.ProposalsRegistry));

            this.ProposalsRegistry.Register<TextProposal>(TextProposal.ProposalType);
            this.ProposalsRegistry.Register<ParameterChangeProposal>(ParameterChangeProposal.ProposalType);
        }
    }
}
