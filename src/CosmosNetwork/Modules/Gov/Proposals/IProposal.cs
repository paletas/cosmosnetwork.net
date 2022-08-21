namespace CosmosNetwork.Modules.Gov.Proposals
{
    public interface IProposal
    {
        internal abstract Serialization.Proposals.IProposal ToSerialization();
    }
}