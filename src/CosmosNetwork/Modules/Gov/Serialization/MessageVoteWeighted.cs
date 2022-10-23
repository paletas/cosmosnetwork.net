using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization
{
    [ProtoContract]
    internal record MessageVoteWeighted(
        [property: ProtoMember(1, Name = "proposal_id")] ulong ProposalId,
        [property: ProtoMember(2, Name = "voter"), JsonPropertyName("voter")] string VoterAddress,
        [property: ProtoMember(3, Name = "options")] WeightedVoteOption[] Options) : SerializerMessage(Gov.MessageVoteWeighted.COSMOS_DESCRIPTOR)
    {
        protected internal override Message ToModel()
        {
            return new Gov.MessageVoteWeighted(
                this.ProposalId,
                this.VoterAddress,
                this.Options.Select(opt => opt.ToModel()).ToArray());
        }
    }

    [ProtoContract]
    internal record WeightedVoteOption(
        [property: ProtoMember(1, Name = "option")] VoteOptionEnum Option,
        [property: ProtoMember(2, Name = "weight")] decimal Weight)
    {
        internal Gov.WeightedVoteOption ToModel()
        {
            return new Gov.WeightedVoteOption(
                (Gov.VoteOptionEnum)this.Option,
                this.Weight);
        }
    }
}

