using CosmosNetwork.Modules.Gov.Serialization.Proposals;

namespace CosmosNetwork.Modules.Upgrade.Serialization.Proposals
{
    public record CancelSoftwareUpgradeProposal(string Title, string Description) : IProposal
    {
        public const string ProposalType = "/cosmos.upgrade.v1beta1.CancelSoftwareUpgradeProposal";

        public string TypeUrl => ProposalType;

        public Gov.Proposals.IProposal ToModel()
        {
            return new Upgrade.Proposals.CancelSoftwareUpgradeProposal(this.Title, this.Description);
        }
    }
}
