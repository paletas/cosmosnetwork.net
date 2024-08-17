using CosmosNetwork.Modules.Gov.Serialization.Proposals;

namespace CosmosNetwork.Modules.Upgrade.Serialization.Proposals
{
    public record SoftwareUpgradeProposal(
        string Title,
        string Description,
        Plan Plan) : IProposal
    {
        public const string ProposalType = "/cosmos.upgrade.v1beta1.SoftwareUpgradeProposal";

        public string TypeUrl => ProposalType;

        public Gov.Proposals.IProposal ToModel()
        {
            return new Upgrade.Proposals.SoftwareUpgradeProposal(this.Title, this.Description, this.Plan.ToModel());
        }
    }
}
