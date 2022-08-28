using ProtoBuf;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum OrderEnum
    {
        [ProtoEnum(Name = "ORDER_NONE_UNSPECIFIED")]
        [EnumMember(Value = "ORDER_NONE_UNSPECIFIED")]
        Unspecified = 0,

        [ProtoEnum(Name = "ORDER_UNORDERED")]
        [EnumMember(Value = "ORDER_UNORDERED")]
        Unordered = 1,

        [ProtoEnum(Name = "ORDER_ORDERED")]
        [EnumMember(Value = "ORDER_ORDERED")]
        Ordered = 2
    }
}
