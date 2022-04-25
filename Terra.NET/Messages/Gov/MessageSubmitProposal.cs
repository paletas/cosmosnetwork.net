using Terra.NET.API.Serialization.Json.Messages.Gov;

namespace Terra.NET.Messages.Gov
{
    public record MessageSubmitProposal(Proposal Proposal, TerraAddress Proposer, Coin[] InitialDeposit)
        : Message(MessageTypeEnum.GovSubmitProposal)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Gov.MessageSubmitProposal(
                this.Proposal.ToJson(),
                this.Proposer.Address,
                this.InitialDeposit.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }

    public enum ProposalTypeEnum
    {
        Text
    }

    public abstract record Proposal(ProposalTypeEnum ProposalType)
    {
        internal abstract NET.API.Serialization.Json.Messages.Gov.IProposal ToJson();
    }

    public record TextProposal(string Title, string Description) : Proposal(ProposalTypeEnum.Text)
    {
        internal override IProposal ToJson()
        {
            return new NET.API.Serialization.Json.Messages.Gov.TextProposal(
                this.Title,
                this.Description);
        }
    }
}