using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Client
{
    [ProtoContract]
    internal record Height(
        [property: ProtoMember(1, Name = "revision_number")] ulong RevisionNumber,
        [property: ProtoMember(2, Name = "revision_height")] ulong RevisionHeight)
    {
        public Ibc.Core.Client.Height ToModel()
        {
            return new Ibc.Core.Client.Height(this.RevisionNumber, this.RevisionHeight);
        }
    }
}
