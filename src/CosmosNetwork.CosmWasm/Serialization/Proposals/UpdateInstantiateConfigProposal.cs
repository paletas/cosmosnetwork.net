using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;

namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    [ProtoContract]
    internal record UpdateInstantiateConfigProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "access_config_updates")] AccessConfigUpdate AccessConfigUpdates) : IProposal
    {
        public const string ProposalType = "/cosmwasm.wasm.v1.UpdateInstantiateConfigProposal";

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmWasm.Proposals.UpdateInstantiateConfigProposal(
                this.Title,
                this.Description,
                this.AccessConfigUpdates.ToModel());
        }
    }
}
