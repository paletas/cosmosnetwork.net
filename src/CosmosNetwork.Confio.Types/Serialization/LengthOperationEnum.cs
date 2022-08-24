using ProtoBuf;

namespace CosmosNetwork.Confio.Serialization
{
    [ProtoContract]
    public enum LengthOperationEnum
    {
        [ProtoEnum(Name = "NO_PREFIX")]
        NoPrefix = 0,

        [ProtoEnum(Name = "VAR_PROTO")]
        VarProto = 1,

        [ProtoEnum(Name = "VAR_RLP")]
        VarRlp = 2,

        [ProtoEnum(Name = "FIXED32_BIG")]
        FixedBig32 = 3,

        [ProtoEnum(Name = "FIXED32_LITTLE")]
        FixedLittle32 = 4,

        [ProtoEnum(Name = "FIXED64_BIG")]
        FixedBig64 = 5,

        [ProtoEnum(Name = "FIXED64_LITTLE")]
        FixedLittle64 = 6,

        [ProtoEnum(Name = "REQUIRE_32_BYTES")]
        RequireBytes32 = 7,

        [ProtoEnum(Name = "REQUIRE_64_BYTES")]
        RequireBytes64 = 8
    }
}
