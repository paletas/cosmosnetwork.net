using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.Modules.Upgrade.Proposals
{
    public record CancelSoftwareUpgradeProposal(string Title, string Description) : IProposal
    {
        public Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Upgrade.Serialization.Proposals.CancelSoftwareUpgradeProposal(this.Title, this.Description);
        }
    }
}
