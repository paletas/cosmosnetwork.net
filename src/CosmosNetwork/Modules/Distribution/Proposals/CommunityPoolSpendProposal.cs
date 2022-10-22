using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.Modules.Distribution.Proposals
{
    public record CommunityPoolSpendProposal(
        string Title,
        string Description,
        CosmosAddress Recipient,
        Coin[] Amount) : IProposal
    {
        public Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.CommunityPoolSpendProposal(this.Title, this.Description, this.Recipient.Address, this.Amount.Select(amt => amt.ToSerialization()).ToArray());
        }
    }
}
