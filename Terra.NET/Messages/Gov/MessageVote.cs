namespace Terra.NET.Messages.Gov
{
    public record MessageVote(ulong ProposalId, TerraAddress Voter, VoteOptionEnum Option)
        : Message(MessageTypeEnum.GovVote)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Gov.MessageVote(
                this.ProposalId,
                this.Voter.Address,
                (NET.API.Serialization.Json.Messages.Gov.VoteOptionEnum)this.Option);
        }
    }

    public enum VoteOptionEnum
    {
        Unspecified = 0,
        Yes = 1,
        Abstain = 2,
        No = 3,
        NoWithVeto = 4
    }
}