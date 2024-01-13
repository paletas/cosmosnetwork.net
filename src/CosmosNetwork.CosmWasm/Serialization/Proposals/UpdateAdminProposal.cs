using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;

namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    [ProtoContract]
    internal record UpdateAdminProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "new_admin")] string NewAdminAddress,
        [property: ProtoMember(4, Name = "contract")] string ContractAddress) : IProposal
    {
        public const string ProposalType = "/cosmwasm.wasm.v1.UpdateAdminProposal";

        public string TypeUrl => ProposalType;

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmWasm.Proposals.UpdateAdminProposal(
                this.Title,
                this.Description,
                this.NewAdminAddress,
                this.ContractAddress);
        }
    }
}
