namespace CosmosNetwork.Ibc.Serialization.Core.Client
{
    internal record Height(ulong RevisionNumber, ulong RevisionHeight)
    {
        public Ibc.Core.Client.Height ToModel()
        {
            return new Ibc.Core.Client.Height(this.RevisionNumber, this.RevisionHeight);
        }
    }
}
