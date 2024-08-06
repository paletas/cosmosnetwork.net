namespace CosmosNetwork.Modules.Gov
{
    public record MessageVote(string MessageType, ulong ProposalId, CosmosAddress Voter, VoteOptionEnum Option) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1.MsgVote";
        public const string COSMOS_BETA_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgVote";

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