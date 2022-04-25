namespace Terra.NET.Messages.Gov
{
    public record MessageVoteWeighted(ulong ProposalId, TerraAddress Voter, WeightedVoteOption[] Options)
        : Message(MessageTypeEnum.GovWeightedVote)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Gov.MessageVoteWeighted(
                this.ProposalId,
                this.Voter.Address,
                this.Options.Select(opt => opt.ToJson()).ToArray());
        }
    }

    public record WeightedVoteOption(VoteOptionEnum Option, decimal Weight)
    {
        internal API.Serialization.Json.Messages.Gov.WeightedVoteOption ToJson()
        {
            return new API.Serialization.Json.Messages.Gov.WeightedVoteOption(
                (API.Serialization.Json.Messages.Gov.VoteOptionEnum)this.Option,
                this.Weight);
        }
    }
}