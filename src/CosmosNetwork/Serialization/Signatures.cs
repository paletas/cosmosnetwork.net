using CosmosNetwork.Serialization.Proto;
using ProtoBuf;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization
{
    [ProtoContract]
    internal class SignatureDescriptor
    {
        [ProtoIgnore]
        public Proto.SimplePublicKey? PublicKey { get; set; }

        [JsonPropertyName("mode_info")]
        [ProtoMember(2, Name = "mode_info")]
        public SignatureMode Data { get; set; } = null!;

        [ProtoMember(3, Name = "sequence")]
        public ulong Sequence { get; set; }

        [JsonIgnore]
        [ProtoMember(1, Name = "public_key")]
        public Any? PublicKeyPacked
        {
            get => this.PublicKey is null ? null : Any.Pack(this.PublicKey);
            set => this.PublicKey = value?.Unpack<Proto.SimplePublicKey>();
        }
    }

    [ProtoContract]
    internal class SignatureMode
    {
        private ProtoBuf.DiscriminatedUnionObject _discriminatedObject = new();

        [ProtoIgnore]
        public ModeInfoDiscriminatorEnum Discriminator => (ModeInfoDiscriminatorEnum)this._discriminatedObject.Discriminator;

        [ProtoMember(1, Name = "single")]
        public SingleMode? Single
        {
            get => this.Discriminator == ModeInfoDiscriminatorEnum.Single ? this._discriminatedObject.Object as SingleMode : null;

            set => this._discriminatedObject = new DiscriminatedUnionObject((int)ModeInfoDiscriminatorEnum.Single, value);
        }

        [ProtoMember(2, Name = "multi")]
        public MultiMode? Multi
        {
            get => this.Discriminator == ModeInfoDiscriminatorEnum.Multi ? this._discriminatedObject.Object as MultiMode : null;

            set => this._discriminatedObject = new DiscriminatedUnionObject((int)ModeInfoDiscriminatorEnum.Multi, value);
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
        [property: ProtoMember(2, Name = "mode_infos")] SignatureMode[] ModeInfos)
    {

    }

    public enum KeyTypeEnum
    {
        Secp256k1,
        Ed25519,
        Multisig
    }
}
