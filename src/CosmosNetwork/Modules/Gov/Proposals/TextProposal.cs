namespace CosmosNetwork.Modules.Gov.Proposals
{
    public record TextProposal(string Title, string Description) : IProposal
    {
        public Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.TextProposal(
                Title,
                Description);
        }
    }
}