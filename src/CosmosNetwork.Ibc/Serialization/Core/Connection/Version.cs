using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Connection
{
    [ProtoContract]
    internal record Version(
        [property: ProtoMember(1, Name = "identifier")] string Identifier,
        [property: ProtoMember(2, Name = "features")] string[] Features)
    {
        public Ibc.Core.Connection.Version ToModel()
        {
            return new Ibc.Core.Connection.Version(this.Identifier, this.Features);
        }
    }
}
