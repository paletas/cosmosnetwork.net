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
            messageRegistry.RegisterMessage<MessageDeposit, Serialization.MessageDeposit>(MessageDeposit.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageSubmitProposal, Serialization.MessageSubmitProposal>(MessageSubmitProposal.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageVote, Serialization.MessageVote>(MessageVote.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageVoteWeighted, Serialization.MessageVoteWeighted>(MessageVoteWeighted.COSMOS_DESCRIPTOR);

            if (cosmosOptions.RegisterBetaMessages)
            {
                messageRegistry.RegisterMessage<MessageDeposit, Serialization.MessageDeposit>(MessageDeposit.COSMOS_BETA_DESCRIPTOR);
                messageRegistry.RegisterMessage<MessageSubmitProposal, Serialization.MessageSubmitProposal>(MessageSubmitProposal.COSMOS_BETA_DESCRIPTOR);
                messageRegistry.RegisterMessage<MessageVote, Serialization.MessageVote>(MessageVote.COSMOS_BETA_DESCRIPTOR);
                messageRegistry.RegisterMessage<MessageVoteWeighted, Serialization.MessageVoteWeighted>(MessageVoteWeighted.COSMOS_BETA_DESCRIPTOR);
            }

            cosmosOptions.JsonSerializerOptions.Converters.Add(new ProposalConverter(this.ProposalsRegistry));

            this.ProposalsRegistry.Register<TextProposal>(TextProposal.ProposalType);
            this.ProposalsRegistry.Register<ParameterChangeProposal>(ParameterChangeProposal.ProposalType);
        }
    }
}
