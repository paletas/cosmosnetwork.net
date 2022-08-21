namespace CosmosNetwork.Modules.Gov
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageVote(ulong ProposalId, CosmosAddress Voter, VoteOptionEnum Option) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgVote";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageVote(
                ProposalId,
                Voter.Address,
                (Serialization.VoteOptionEnum)Option);
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