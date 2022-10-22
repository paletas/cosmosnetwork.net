using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.CosmWasm.Proposals
{
    public record UnpinCodesProposal(
        string Title,
        string Description,
        ulong CodeIds) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.UnpinCodesProposal(
                this.Title,
                this.Description,
                this.CodeIds);
        }
    }
}
