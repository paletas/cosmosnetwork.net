using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.Ibc.Core.Client.Proposals
{
    public record ClientUpdateProposal(
        string Title,
        string Description,
        string SubjectClientId,
        string SubstituteClientId) : IProposal
    {
        public Modules.Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Core.Client.Proposals.ClientUpdateProposal(
                this.Title,
                this.Description,
                this.SubjectClientId,
                this.SubstituteClientId);
        }
    }
}
