namespace CosmosNetwork.Ibc.Core.Connection
{
    public record Version(
        string Identifier,
        string[] Features)
    {
        internal Serialization.Core.Connection.Version ToSerialization()
        {
            return new Serialization.Core.Connection.Version(this.Identifier, this.Features);
        }
    }
}
