using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Distribution.Serialization.Proposals
{
    [ProtoContract]
    internal record CommunityPoolSpendProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "recipient")] string RecipientAddress,
        [property: ProtoMember(4, Name = "amount")] DenomAmount[] Amount) : IProposal
    {
        public const string ProposalType = "/cosmos.distribution.v1beta1.CommunityPoolSpendProposal";

        public Gov.Proposals.IProposal ToModel()
        {
            return new Distribution.Proposals.CommunityPoolSpendProposal(this.Title, this.Description, this.RecipientAddress, this.Amount.Select(amt => amt.ToModel()).ToArray());
        }
    }
}
