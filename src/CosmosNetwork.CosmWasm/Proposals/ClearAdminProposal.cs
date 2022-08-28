using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.CosmWasm.Proposals
{
    public record ClearAdminProposal(
        string Title,
        string Description,
        CosmosAddress Contract) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.ClearAdminProposal(
                this.Title,
                this.Description,
                this.Contract);
        }
    }
}
