namespace CosmosNetwork.Modules.Gov
{
    public record ProposalTally(
        Int128 Yes,
        Int128 Abstain,
        Int128 No,
        Int128 NoWithVeto)
    {
        internal Serialization.TallyResult ToSerialization()
        {
            return new Serialization.TallyResult
            {
                Yes = this.Yes,
                Abstain = this.Abstain,
                No = this.No,
                NoWithVeto = this.NoWithVeto,
            };
        }
    }
}
