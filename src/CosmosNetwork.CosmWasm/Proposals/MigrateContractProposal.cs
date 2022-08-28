using CosmosNetwork.Modules.Gov.Proposals;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm.Proposals
{
    public record MigrateContractProposal(
        string Title,
        string Description,
        CosmosAddress Contract,
        ulong CodeId,
        string Message) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.MigrateContractProposal(
                this.Title,
                this.Description,
                this.Contract,
                this.CodeId,
                JsonDocument.Parse(this.Message));
        }
    }
}
