using ProtoBuf;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum BlockIdFlagEnum
    {
        [ProtoEnum(Name = "BLOCK_ID_FLAG_UNKNOWN")]
        [EnumMember(Value = "BLOCK_ID_FLAG_UNKNOWN")]
        Unknown = 0,

        [ProtoEnum(Name = "BLOCK_ID_FLAG_ABSENT")]
        [EnumMember(Value = "BLOCK_ID_FLAG_ABSENT")]
        Absent = 1,

        [ProtoEnum(Name = "BLOCK_ID_FLAG_COMMIT")]
        [EnumMember(Value = "BLOCK_ID_FLAG_COMMIT")]
        Commit = 2,

        [ProtoEnum(Name = "BLOCK_ID_FLAG_NIL")]
        [EnumMember(Value = "BLOCK_ID_FLAG_NIL")]
        Nil = 3
    }
}
