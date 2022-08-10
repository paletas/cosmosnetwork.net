using CosmosNetwork.Serialization.Messages.Gov;

namespace CosmosNetwork.Messages.Gov
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageSubmitProposal(Proposal Proposal, CosmosAddress Proposer, Coin[] InitialDeposit) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgSubmitProposal";

        protected internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Gov.MessageSubmitProposal(
                Proposal.ToJson(),
                Proposer.Address,
                InitialDeposit.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }

    public enum ProposalTypeEnum
    {
        Text,
        CommunitySpend
    }

    public abstract record Proposal(ProposalTypeEnum ProposalType)
    {
        internal abstract Serialization.Messages.Gov.IProposal ToJson();
    }

    public record TextProposal(string Title, string Description) : Proposal(ProposalTypeEnum.Text)
    {
        internal override IProposal ToJson()
        {
            return new Serialization.Messages.Gov.TextProposal(
                Title,
                Description);
        }
    }

    public record CommunitySpendProposal(string Title, string Description, CosmosAddress Recipient, Coin[] Amount) : Proposal(ProposalTypeEnum.CommunitySpend)
    {
        internal override IProposal ToJson()
        {
            return new Serialization.Messages.Gov.CommunitySpendProposal(
                Title,
                Description,
                Recipient.Address,
                Amount.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }
}