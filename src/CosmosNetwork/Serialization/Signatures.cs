using CosmosNetwork.Serialization.Json.Converters;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization
{
    internal class SignatureDescriptor
    {
        [ProtoIgnore]
        public Tendermint.PublicKey PublicKey { get; set; } = null!;

        [ProtoMember(2, Name = "data")]
        public SignatureData Data { get; set; } = null!;

        [ProtoMember(3, Name = "sequence")]
        public ulong Sequence { get; set; }

        [ProtoMember(1, Name = "public_key")]
        public Any PublicKeyPacked
        {
            get => Any.Pack(PublicKey);
            set => PublicKey = value.Unpack<Tendermint.PublicKey>() ?? throw new InvalidOperationException();
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

    [ProtoContract]
    internal enum SignModeEnum
    {
        [ProtoEnum(Name = "SIGN_MODE_UNSPECIFIED")]
        Unspecified = 0,

        [ProtoEnum(Name = "SIGN_MODE_DIRECT")]
        Direct = 1,

        [ProtoEnum(Name = "SIGN_MODE_TEXTUAL")]
        Textual = 2,

        [ProtoEnum(Name = "SIGN_MODE_LEGACY_AMINO_JSON")]
        LegacyAmino = 127,

        [ProtoEnum(Name = "SIGN_MODE_EIP_191")]
        Eip191 = 191
    }

    [ProtoContract]
    internal record MultiMode(
        [property: ProtoMember(1, Name = "bitarray")] CompactBitArray BitArray,
        [property: ProtoMember(2, Name = "mode_infos")] SignatureData[] ModeInfos)
    {

    }

    internal record SignerOptions(
        [property: JsonPropertyName("pub_key"), JsonConverter(typeof(PublicKeyConverter))] PublicKey PublicKey,
        string Signature)
    {
        public TransactionSignature ToModel()
        {
            SignatureKey publicKey = PublicKey.ToModel();
            return new CosmosNetwork.TransactionSignature(publicKey, Signature);
        }
    }

    public enum KeyTypeEnum
    {
        Secp256k1,
        Ed25519,
        Multisig
    }
}
