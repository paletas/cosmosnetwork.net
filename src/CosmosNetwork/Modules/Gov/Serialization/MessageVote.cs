using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization
{
    internal record MessageVote(
        ulong ProposalId,
        [property: JsonPropertyName("voter")] string VoterAddress,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] VoteOptionEnum Option) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "gov/MsgVote";

        protected internal override Message ToModel()
        {
            return new Gov.MessageVote(ProposalId, VoterAddress, (Gov.VoteOptionEnum)Option);
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

