using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;

namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    [ProtoContract]
    internal record ClearAdminProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "contract")] string ContractAddress) : IProposal
    {
        public const string ProposalType = "/cosmwasm.wasm.v1.ClearAdminProposal";

        public string TypeUrl => ProposalType;

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmWasm.Proposals.ClearAdminProposal(
                this.Title,
                this.Description,
                this.ContractAddress);
        }
    }
}
