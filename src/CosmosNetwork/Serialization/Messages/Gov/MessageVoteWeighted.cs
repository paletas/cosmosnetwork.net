using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Gov
{
    internal record MessageVoteWeighted(
        ulong ProposalId,
        [property: JsonPropertyName("voter")] string VoterAddress,
        WeightedVoteOption[] Options) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Gov.MessageVoteWeighted(
                ProposalId,
                VoterAddress,
                Options.Select(opt => opt.ToModel()).ToArray());
        }
    }

    internal record WeightedVoteOption(VoteOptionEnum Option, decimal Weight)
    {
        internal CosmosNetwork.Messages.Gov.WeightedVoteOption ToModel()
        {
            return new CosmosNetwork.Messages.Gov.WeightedVoteOption(
                (CosmosNetwork.Messages.Gov.VoteOptionEnum)Option,
                Weight);
        }
    }
}

