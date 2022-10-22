using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.CosmWasm.Proposals
{
    public record StoreCodeProposal(
        string Title,
        string Description,
        CosmosAddress RunAs,
        byte[] WasmByteCode,
        AccessConfig InstantiatePermission) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.StoreCodeProposal(
                this.Title,
                this.Description,
                this.RunAs,
                this.WasmByteCode,
                this.InstantiatePermission.ToSerialization());
        }
    }
}
