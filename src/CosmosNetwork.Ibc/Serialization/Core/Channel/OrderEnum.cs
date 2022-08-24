using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    public enum OrderEnum
    {
        [ProtoEnum(Name = "ORDER_NONE_UNSPECIFIED")]
        Unspecified = 0,

        [ProtoEnum(Name = "ORDER_UNORDERED")]
        Unordered = 1,
        [ProtoEnum(Name = "ORDER_ORDERED")]
        Ordered = 2
    }
}
