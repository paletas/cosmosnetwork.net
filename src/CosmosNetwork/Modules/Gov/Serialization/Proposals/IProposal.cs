using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Modules.Gov.Serialization.Proposals
{
    public interface IProposal : IHasAny
    {
        Gov.Proposals.IProposal ToModel();
    }
}
