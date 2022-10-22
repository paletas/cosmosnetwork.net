using CosmosNetwork.Ibc.Serialization.Core.Commitment;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Connection
{
    [ProtoContract]
    internal record Counterparty(
        [property: ProtoMember(1, Name = "client_id")] string ClientId,
        [property: ProtoMember(2, Name = "connection_id")] string ConnectionId,
        [property: ProtoMember(3, Name = "prefix")] MerklePrefix Prefix)
    {
        public Ibc.Core.Connection.Counterparty ToModel()
        {
            return new Ibc.Core.Connection.Counterparty(this.ClientId, this.ConnectionId, this.Prefix.ToModel());
        }
    }
}
