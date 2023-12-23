namespace CosmosNetwork.Modules.Gov
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageVote(ulong ProposalId, CosmosAddress Voter, VoteOptionEnum Option) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgVote";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageVote(
                this.ProposalId,
                this.Voter.Address,
                (Serialization.VoteOptionEnum)this.Option);
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