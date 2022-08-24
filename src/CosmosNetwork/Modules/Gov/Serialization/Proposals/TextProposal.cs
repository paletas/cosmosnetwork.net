namespace CosmosNetwork.Modules.Gov.Serialization.Proposals
{
    internal record TextProposal(string Title, string Description) : IProposalImplementation
    {
        private const string ProposalType = "cosmos.gov.v1beta1.TextProposal";

        public string TypeUrl => ProposalType;

        public Gov.Proposals.IProposal ToModel()
        {
            return new Gov.Proposals.TextProposal(Title, Description);
        }
    }
}
