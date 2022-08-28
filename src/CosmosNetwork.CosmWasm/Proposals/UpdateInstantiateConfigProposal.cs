using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.CosmWasm.Proposals
{
    public record UpdateInstantiateConfigProposal(
        string Title,
        string Description,
        AccessConfigUpdate AccessConfigUpdates) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.UpdateInstantiateConfigProposal(
                this.Title,
                this.Description,
                this.AccessConfigUpdates.ToSerialization());
        }
    }
}
