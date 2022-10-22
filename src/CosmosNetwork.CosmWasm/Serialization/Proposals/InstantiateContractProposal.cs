using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    [ProtoContract]
    internal record InstantiateContractProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "run_as")] string RunAsAddress,
        [property: ProtoMember(4, Name = "admin")] string? AdminAddress,
        [property: ProtoMember(5, Name = "code_id")] ulong CodeId,
        [property: ProtoMember(6, Name = "label")] string Label,
        [property: ProtoMember(7, Name = "msg")] JsonDocument Message,
        [property: ProtoMember(8, Name = "funds")] DenomAmount[] Funds) : IProposal
    {
        public const string ProposalType = "/cosmwasm.wasm.v1.InstantiateContractProposal";

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmWasm.Proposals.InstantiateContractProposal(
                this.Title,
                this.Description,
                this.RunAsAddress,
                this.AdminAddress,
                this.CodeId,
                this.Label,
                JsonSerializer.Serialize(this.Message),
                this.Funds.Select(f => f.ToModel()).ToArray());
        }
    }
}
