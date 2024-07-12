using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.Modules.Upgrade.Proposals
{
    public record SoftwareUpgradeProposal(
        string Title,
        string Description,
        Plan Plan) : IProposal
    {
        public Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Upgrade.Serialization.Proposals.SoftwareUpgradeProposal(this.Title, this.Description, this.Plan.ToSerialization());
        }
    }
}
