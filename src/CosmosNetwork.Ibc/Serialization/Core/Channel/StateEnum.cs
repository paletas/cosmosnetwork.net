using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    public enum StateEnum
    {
        [ProtoEnum(Name = "STATE_UNINITIALIZED_UNSPECIFIED")]
        Unitilialized = 0,
        [ProtoEnum(Name = "STATE_UNINITIALIZED_UNSPECIFIED")]
        Unspecified = 0,

        [ProtoEnum(Name = "STATE_INIT")]
        Init = 1,
        [ProtoEnum(Name = "STATE_TRYOPEN")]
        TryOpen = 2,
        [ProtoEnum(Name = "STATE_OPEN")]
        Open = 3,
        [ProtoEnum(Name = "STATE_CLOSED")]
        Closed = 4
    }
}
