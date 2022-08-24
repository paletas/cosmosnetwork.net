using ProtoBuf;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public enum BlockIdFlagEnum
    {
        [ProtoEnum(Name = "BLOCK_ID_FLAG_UNKNOWN")]
        Unknown = 0,

        [ProtoEnum(Name = "BLOCK_ID_FLAG_ABSENT")]
        Absent = 1,

        [ProtoEnum(Name = "BLOCK_ID_FLAG_COMMIT")]
        Commit = 2,

        [ProtoEnum(Name = "BLOCK_ID_FLAG_NIL")]
        Nil = 3
    }
}
