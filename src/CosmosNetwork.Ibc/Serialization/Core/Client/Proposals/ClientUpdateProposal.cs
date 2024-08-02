using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Client.Proposals
{
    [ProtoContract]
    public record ClientUpdateProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "subject_client_id")] string SubjectClientId,
        [property: ProtoMember(4, Name = "substitute_client_id")] string SubstituteClientId) : IProposal
    {
        public const string ProposalType = "/ibc.core.client.v1.ClientUpdateProposal";

        public string TypeUrl => ProposalType;

        public Modules.Gov.Proposals.IProposal ToModel()
        {
            return new CosmosNetwork.Ibc.Core.Client.Proposals.ClientUpdateProposal(
                this.Title,
                this.Description,
                this.SubjectClientId,
                this.SubstituteClientId);
        }
    }
}
