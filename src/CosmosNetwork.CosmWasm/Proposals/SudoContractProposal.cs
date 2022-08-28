using CosmosNetwork.Modules.Gov.Proposals;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm.Proposals
{
    public record SudoContractProposal(
        string Title,
        string Description,
        CosmosAddress Contract,
        string Message) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.SudoContractProposal(
                this.Title,
                this.Description,
                this.Contract,
                JsonDocument.Parse(this.Message));
        }
    }
}
