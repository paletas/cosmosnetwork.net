namespace CosmosNetwork.Messages.Gov
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageVoteWeighted(ulong ProposalId, CosmosAddress Voter, WeightedVoteOption[] Options) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgVoteWeighted";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Gov.MessageVoteWeighted(
                ProposalId,
                Voter.Address,
                Options.Select(opt => opt.ToJson()).ToArray());
        }
    }

    public record WeightedVoteOption(VoteOptionEnum Option, decimal Weight)
    {
        internal Serialization.Messages.Gov.WeightedVoteOption ToJson()
        {
            return new Serialization.Messages.Gov.WeightedVoteOption(
                (Serialization.Messages.Gov.VoteOptionEnum)Option,
                Weight);
        }
    }
}