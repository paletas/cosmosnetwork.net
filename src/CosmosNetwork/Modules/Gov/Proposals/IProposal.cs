namespace CosmosNetwork.Modules.Gov.Proposals
{
    public interface IProposal
    {
        string Title { get; }

        string Description { get; }

        Serialization.Proposals.IProposal ToSerialization();
    }
}