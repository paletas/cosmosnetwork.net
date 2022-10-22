using CosmosNetwork.Modules.Gov.Proposals;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm.Proposals
{
    public record InstantiateContractProposal(
        string Title,
        string Description,
        CosmosAddress RunAs,
        CosmosAddress? Admin,
        ulong CodeId,
        string Label,
        string Message,
        Coin[] Funds) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.InstantiateContractProposal(
                this.Title,
                this.Description,
                this.RunAs,
                this.Admin,
                this.CodeId,
                this.Label,
                JsonDocument.Parse(this.Message),
                this.Funds.Select(f => f.ToSerialization()).ToArray());
        }
    }
}
