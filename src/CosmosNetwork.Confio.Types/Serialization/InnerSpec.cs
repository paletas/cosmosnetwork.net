using ProtoBuf;

namespace CosmosNetwork.Confio.Types.Serialization
{
    [ProtoContract]
    public record InnerSpec(
        [property: ProtoMember(1, Name = "child_order")] int[] ChildOrder,
        [property: ProtoMember(2, Name = "child_size")] int ChildSize,
        [property: ProtoMember(3, Name = "min_prefix_length")] int MinPrefixLength,
        [property: ProtoMember(4, Name = "max_prefix_length")] int MaxPrefixLength,
        [property: ProtoMember(5, Name = "empty_child")] byte[] EmptyChild,
        [property: ProtoMember(6, Name = "hash")] HashOperationEnum Hash)
    {
        public Types.InnerSpec ToModel()
        {
            return new Types.InnerSpec(
                ChildOrder,
                ChildSize,
                MinPrefixLength,
                MaxPrefixLength,
                EmptyChild,
                (Types.HashOperationEnum)Hash);
        }
    }
}
