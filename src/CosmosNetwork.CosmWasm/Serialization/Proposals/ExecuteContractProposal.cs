using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    [ProtoContract]
    internal record ExecuteContractProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "run_as")] string RunAsAddress,
        [property: ProtoMember(4, Name = "contract")] string ContractAddress,
        [property: ProtoMember(5, Name = "msg")] JsonDocument Message) : IProposal
    {
        public const string ProposalType = "/cosmwasm.wasm.v1.ExecuteContractProposal";

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmWasm.Proposals.ExecuteContractProposal(
                this.Title,
                this.Description,
                this.RunAsAddress,
                this.ContractAddress,
                JsonSerializer.Serialize(this.Message));
        }
    }
}
