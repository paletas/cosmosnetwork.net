using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization
{
    internal record MessageVoteWeighted(
        ulong ProposalId,
        [property: JsonPropertyName("voter")] string VoterAddress,
        WeightedVoteOption[] Options) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new Gov.MessageVoteWeighted(
                ProposalId,
                VoterAddress,
                Options.Select(opt => opt.ToModel()).ToArray());
        }
    }

    internal record WeightedVoteOption(VoteOptionEnum Option, decimal Weight)
    {
        internal Gov.WeightedVoteOption ToModel()
        {
            return new Gov.WeightedVoteOption(
                (Gov.VoteOptionEnum)Option,
                Weight);
        }
    }
}

