namespace CosmosNetwork.Modules.Gov.Proposals
{
    public interface IProposal
    {
        Serialization.Proposals.IProposal ToSerialization();
    }
}