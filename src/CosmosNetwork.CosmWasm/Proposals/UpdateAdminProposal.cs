using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.CosmWasm.Proposals
{
    public record UpdateAdminProposal(
        string Title,
        string Description,
        CosmosAddress NewAdmin,
        CosmosAddress Contract) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.UpdateAdminProposal(
                this.Title,
                this.Description,
                this.NewAdmin,
                this.Contract);
        }
    }
}
