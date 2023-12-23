using ProtoBuf;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum StateEnum
    {
        [ProtoEnum(Name = "STATE_UNINITIALIZED_UNSPECIFIED")]
        [EnumMember(Value = "STATE_UNINITIALIZED_UNSPECIFIED")]
        Unitilialized = 0,

        [ProtoEnum(Name = "STATE_INIT")]
        [EnumMember(Value = "STATE_INIT")]
        Init = 1,
        [ProtoEnum(Name = "STATE_TRYOPEN")]
        [EnumMember(Value = "STATE_TRYOPEN")]
        TryOpen = 2,
        [ProtoEnum(Name = "STATE_OPEN")]
        [EnumMember(Value = "STATE_OPEN")]
        Open = 3,
        [ProtoEnum(Name = "STATE_CLOSED")]
        [EnumMember(Value = "STATE_CLOSED")]
        Closed = 4
    }
}
