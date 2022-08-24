using ProtoBuf;

namespace CosmosNetwork.Confio.Serialization
{
    [ProtoContract]
    public enum HashOperationEnum
    {
        [ProtoEnum(Name = "NO_HASH")]
        NoHash = 0,

        [ProtoEnum(Name = "SHA256")]
        SHA256 = 1,

        [ProtoEnum(Name = "SHA512")]
        SHA512 = 2,

        [ProtoEnum(Name = "KECCAK")]
        KECCAK = 3,

        [ProtoEnum(Name = "RIPEMD160")]
        RIPEMD160 = 4,

        [ProtoEnum(Name = "BITCOIN")]
        BITCOIN = 5
    }
}
