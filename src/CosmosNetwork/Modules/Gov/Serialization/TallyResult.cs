namespace CosmosNetwork.Modules.Gov.Serialization
{
    internal class TallyResult
    {
        public Int128 Abstain { get; set; }

        public Int128 No { get; set; }

        public Int128 NoWithVeto { get; set; }

        public Int128 Yes { get; set; }

        public Gov.ProposalTally ToModel()
        {
            return new Gov.ProposalTally(
                this.Yes,
                this.Abstain,
                this.No,
                this.NoWithVeto);
        }
    }
}
