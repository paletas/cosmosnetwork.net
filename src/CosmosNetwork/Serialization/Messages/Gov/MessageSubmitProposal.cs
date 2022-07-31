using CosmosNetwork.Messages.Gov;
using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Gov
{
    internal record MessageSubmitProposal(
        [property: JsonConverter(typeof(ProposalConverter))] IProposal Content,
        [property: JsonPropertyName("proposer")] string ProposerAddress,
        DenomAmount[] InitialDeposit) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "gov/MsMsgSubmitProposalSwap";

        internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Gov.MessageSubmitProposal(
                Content.ToModel(),
                ProposerAddress,
                InitialDeposit.Select(c => c.ToModel()).ToArray());
        }
    }

    internal interface IProposal
    {
        Proposal ToModel();
    }

    internal record TextProposal(string Title, string Description) : IProposal
    {
        public Proposal ToModel()
        {
            return new CosmosNetwork.Messages.Gov.TextProposal(Title, Description);
        }
    }

    internal record CommunitySpendProposal(string Title, string Description, string Recipient, DenomAmount[] Amount) : IProposal
    {
        public Proposal ToModel()
        {
            return new CosmosNetwork.Messages.Gov.CommunitySpendProposal(Title, Description, Recipient, Amount.Select(coin => coin.ToModel()).ToArray());
        }
    }
}
