using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization.Proposals
{
    public interface IProposal
    {
        Gov.Proposals.IProposal ToModel();
    }

    internal interface IProposalImplementation : IProposal, IHasAny
    {

    }
}
