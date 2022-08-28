using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;

namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    [ProtoContract]
    internal record StoreCodeProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "run_as")] string RunAsAddress,
        [property: ProtoMember(4, Name = "wasm_byte_code")] byte[] WasmByteCode,
        [property: ProtoMember(7, Name = "instantiate_permission")] AccessConfig InstantiatePermission) : IProposal
    {
        public const string ProposalType = "/cosmwasm.wasm.v1.StoreCodeProposal";

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmWasm.Proposals.StoreCodeProposal(
                this.Title,
                this.Description,
                this.RunAsAddress,
                this.WasmByteCode,
                this.InstantiatePermission.ToModel());
        }
    }
}
