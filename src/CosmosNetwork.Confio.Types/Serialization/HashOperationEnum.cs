using ProtoBuf;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Confio.Serialization
{
    [ProtoContract]
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum HashOperationEnum
    {
        [ProtoEnum(Name = "NO_HASH")]
        [EnumMember(Value = "NO_HASH")]
        NoHash = 0,

        [ProtoEnum(Name = "SHA256")]
        [EnumMember(Value = "SHA256")]
        SHA256 = 1,

        [ProtoEnum(Name = "SHA512")]
        [EnumMember(Value = "SHA512")]
        SHA512 = 2,

        [ProtoEnum(Name = "KECCAK")]
        [EnumMember(Value = "KECCAK")]
        KECCAK = 3,

        [ProtoEnum(Name = "RIPEMD160")]
        [EnumMember(Value = "RIPEMD160")]
        RIPEMD160 = 4,

        [ProtoEnum(Name = "BITCOIN")]
        [EnumMember(Value = "BITCOIN")]
        BITCOIN = 5
    }
}
