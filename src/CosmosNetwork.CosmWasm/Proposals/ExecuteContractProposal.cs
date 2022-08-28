using CosmosNetwork.Modules.Gov.Proposals;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm.Proposals
{
    public record ExecuteContractProposal(
        string Title,
        string Description,
        CosmosAddress RunAs,
        CosmosAddress Contract,
        string Message) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.ExecuteContractProposal(
                this.Title,
                this.Description,
                this.RunAs,
                this.Contract,
                JsonDocument.Parse(this.Message));
        }
    }
}
