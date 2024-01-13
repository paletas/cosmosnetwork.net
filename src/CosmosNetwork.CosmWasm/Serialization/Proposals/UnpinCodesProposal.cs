using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;

namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    [ProtoContract]
    internal record UnpinCodesProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "code_ids")] ulong CodeIds) : IProposal
    {
        public const string ProposalType = "/cosmwasm.wasm.v1.UnpinCodesProposal";

        public string TypeUrl => ProposalType;

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmWasm.Proposals.UnpinCodesProposal(
                this.Title,
                this.Description,
                this.CodeIds);
        }
    }
}
