using ProtoBuf;

namespace CosmosNetwork.Serialization
{
    [ProtoContract]
    internal record CompactBitArray(
        [property: ProtoMember(1, Name = "extra_bits_stored")] uint ExtraBitsStored,
        [property: ProtoMember(2, Name = "elems")] byte[] Elems)
    {

    }
}
