using CosmosNetwork.Ibc.Core.Commitment;

namespace CosmosNetwork.Ibc.Core.Connection
{
    public record Counterparty(
        string ClientId,
        string ConnectionId,
        MerklePrefix Prefix)
    {
        internal Serialization.Core.Connection.Counterparty ToSerialization()
        {
            return new Serialization.Core.Connection.Counterparty(this.ClientId, this.ConnectionId, this.Prefix.ToSerialization());
        }
    }
}
