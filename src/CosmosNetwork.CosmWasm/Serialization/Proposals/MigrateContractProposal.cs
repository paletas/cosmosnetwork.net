using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    [ProtoContract]
    internal record MigrateContractProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "contract")] string ContractAddress,
        [property: ProtoMember(4, Name = "code_id")] ulong CodeId,
        [property: ProtoMember(5, Name = "msg")] JsonDocument Message) : IProposal
    {
        public const string ProposalType = "/cosmwasm.wasm.v1.MigrateContractProposal";

        public string TypeUrl => ProposalType;

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmWasm.Proposals.MigrateContractProposal(
                this.Title,
                this.Description,
                this.ContractAddress,
                this.CodeId,
                JsonSerializer.Serialize(this.Message));
        }
    }
}
