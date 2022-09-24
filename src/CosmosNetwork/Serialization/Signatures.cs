using CosmosNetwork.Serialization.Json.Converters;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization
{
    internal class SignatureDescriptor
    {
        [ProtoIgnore]
        public Proto.PublicKey PublicKey { get; set; } = null!;

        [JsonPropertyName("mode_info")]
        [ProtoMember(2, Name = "data")]
        public SignatureData Data { get; set; } = null!;

        [ProtoMember(3, Name = "sequence")]
        public ulong Sequence { get; set; }

        [JsonIgnore]
        [ProtoMember(1, Name = "public_key")]
        public Any PublicKeyPacked
        {
            get => Any.Pack(PublicKey);
            set => PublicKey = value.Unpack<Proto.PublicKey>() ?? throw new InvalidOperationException();
        }
    }

    [ProtoContract]
    internal class SignatureData
    {
        private ProtoBuf.DiscriminatedUnionObject _discriminatedObject = new();

        public ModeInfoDiscriminatorEnum Discriminator => (ModeInfoDiscriminatorEnum)_discriminatedObject.Discriminator;

        [ProtoMember(2, Name = "single")]
        public SingleMode? Single
        {
            get => Discriminator == ModeInfoDiscriminatorEnum.Single ? _discriminatedObject.Object as SingleMode : null;

            set => _discriminatedObject = new DiscriminatedUnionObject((int)ModeInfoDiscriminatorEnum.Single, value);
        }

        [ProtoMember(3, Name = "multi")]
        public MultiMode? Multi
        {
            get => Discriminator == ModeInfoDiscriminatorEnum.Multi ? _discriminatedObject.Object as MultiMode : null;

            set => _discriminatedObject = new DiscriminatedUnionObject((int)ModeInfoDiscriminatorEnum.Multi, value);
        }
    }

    [ProtoContract]
    internal enum ModeInfoDiscriminatorEnum
    {
        Unassigned = 0,
        Single = 1,
        Multi = 2
    }

    [ProtoContract]
    internal record SingleMode(
        [property: ProtoMember(1, Name = "mode")] SignModeEnum Mode)
    {

    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    [ProtoContract]
    internal enum SignModeEnum
    {
        [EnumMember(Value = "SIGN_MODE_UNSPECIFIED")] 
        [ProtoEnum(Name = "SIGN_MODE_UNSPECIFIED")]
        Unspecified = 0,

        [EnumMember(Value = "SIGN_MODE_DIRECT")]
        [ProtoEnum(Name = "SIGN_MODE_DIRECT")]
        Direct = 1,

        [EnumMember(Value = "SIGN_MODE_TEXTUAL")]
        [ProtoEnum(Name = "SIGN_MODE_TEXTUAL")]
        Textual = 2,

        [EnumMember(Value = "SIGN_MODE_LEGACY_AMINO_JSON")]
        [ProtoEnum(Name = "SIGN_MODE_LEGACY_AMINO_JSON")]
        LegacyAmino = 127,

        [EnumMember(Value = "SIGN_MODE_EIP_191")]
        [ProtoEnum(Name = "SIGN_MODE_EIP_191")]
        Eip191 = 191
    }

    [ProtoContract]
    internal record MultiMode(
        [property: ProtoMember(1, Name = "bitarray")] CompactBitArray BitArray,
        [property: ProtoMember(2, Name = "mode_infos")] SignatureData[] ModeInfos)
    {

    }

    public enum KeyTypeEnum
    {
        Secp256k1,
        Ed25519,
        Multisig
    }
}
