using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    [ProtoContract]
    internal record SudoContractProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "contract")] string ContractAddress,
        [property: ProtoMember(4, Name = "msg")] JsonDocument Message) : IProposal
    {
        public const string ProposalType = "/cosmwasm.wasm.v1.SudoContractProposal";

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmWasm.Proposals.SudoContractProposal(
                this.Title,
                this.Description,
                this.ContractAddress,
                JsonSerializer.Serialize(this.Message));
        }
    }
}
