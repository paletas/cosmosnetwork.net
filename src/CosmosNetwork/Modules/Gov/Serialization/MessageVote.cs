using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization
{
    [ProtoContract]
    internal record MessageVote(
        [property: ProtoMember(1, Name = "proposal_id")] ulong ProposalId,
        [property: ProtoMember(2, Name = "voter"), JsonPropertyName("voter")] string VoterAddress,
        [property: ProtoMember(3, Name = "option"), JsonConverter(typeof(JsonStringEnumConverter))] VoteOptionEnum Option) : SerializerMessage(Gov.MessageVote.COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "gov/MsgVote";

        protected internal override Message ToModel()
        {
            return new Gov.MessageVote(this.ProposalId, this.VoterAddress, (Gov.VoteOptionEnum)this.Option);
        }
    }

    internal enum VoteOptionEnum
    {
        VOTE_OPTION_UNSPECIFIED = 0,
        VOTE_OPTION_YES = 1,
        VOTE_OPTION_ABSTAIN = 2,
        VOTE_OPTION_NO = 3,
        VOTE_OPTION_NO_WITH_VETO = 4
    }
}

