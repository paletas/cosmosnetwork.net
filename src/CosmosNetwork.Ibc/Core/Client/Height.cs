namespace CosmosNetwork.Ibc.Core.Client
{
    public record Height(ulong RevisionNumber, ulong RevisionHeight)
    {
        internal Serialization.Core.Client.Height ToSerialization()
        {
            return new Serialization.Core.Client.Height(this.RevisionNumber, this.RevisionHeight);
        }
    }
}
