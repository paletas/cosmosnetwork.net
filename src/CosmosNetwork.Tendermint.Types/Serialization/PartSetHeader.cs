using ProtoBuf;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public record PartSetHeader(
        [property: ProtoMember(1, Name = "total")] uint Total,
        [property: ProtoMember(2, Name = "hash")] byte[] Hash)
    {
        public Types.PartSetHeader ToModel()
        {
            return new Types.PartSetHeader(this.Total, this.Hash);
        }
    }
}
