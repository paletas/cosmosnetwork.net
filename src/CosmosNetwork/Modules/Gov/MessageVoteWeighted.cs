namespace CosmosNetwork.Modules.Gov
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageVoteWeighted(ulong ProposalId, CosmosAddress Voter, WeightedVoteOption[] Options) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgVoteWeighted";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageVoteWeighted(
                this.ProposalId,
                this.Voter.Address,
                this.Options.Select(opt => opt.ToJson()).ToArray());
        }
    }

    public record WeightedVoteOption(VoteOptionEnum Option, decimal Weight)
    {
        internal Serialization.WeightedVoteOption ToJson()
        {
            return new Serialization.WeightedVoteOption(
                (Serialization.VoteOptionEnum)this.Option,
                this.Weight);
        }
    }
}