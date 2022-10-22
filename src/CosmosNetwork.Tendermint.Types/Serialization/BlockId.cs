using ProtoBuf;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public record BlockId(
        [property: ProtoMember(1, Name = "hash")] byte[] Hash,
        [property: ProtoMember(2, Name = "part_set_header")] PartSetHeader PartSetHeader)
    {
        internal Types.BlockId ToModel()
        {
            return new Types.BlockId(this.Hash, this.PartSetHeader.ToModel());
        }
    }
}
