using Cosmos.SDK.Protos.Crypto.Keys.Multisig;
using Cosmos.SDK.Protos.Crypto.Keys.Secp256k1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Text.Json.Serialization;
using Terra.NET.API.Serialization.Json.Converters;

namespace Terra.NET.API.Serialization.Json
{
    internal record SignerInfo([property: JsonConverter(typeof(PublicKeyConverter))] PublicKey PublicKey, SignerModeInfo ModeInfo);

    internal record SignerModeInfo(SignerSingleMode Single);

    internal record SignerSingleMode(SignerModeEnum Mode);

    internal record SignerOptions([property: JsonPropertyName("pub_key"), JsonConverter(typeof(PublicKeyConverter))] PublicKey PublicKey, string Signature)
    {
        public NET.TransactionSignature ToModel()
        {
            var publicKey = this.PublicKey.ToModel();
            return new NET.TransactionSignature(publicKey, this.Signature);
        }
    }

    internal enum KeyTypeEnum
    {
        Secp256k1,
        Ed25519,
        Multisig
    }

    internal abstract record PublicKey(KeyTypeEnum Type)
    {
        public abstract NET.SignatureKey ToModel();

        public abstract IMessage ToProto();

        public Any PackAny()
        {
            return Any.Pack(ToProto(), string.Empty);
        }
    }

    internal record Secp256k1([property: JsonPropertyName("key")] string Value) : PublicKey(KeyTypeEnum.Secp256k1)
    {
        public override IMessage ToProto()
        {
            return new PubKey
            {
                Key = ByteString.FromBase64(this.Value)
            };
        }

        public override NET.SignatureKey ToModel()
        {
            return new NET.Secp256k1Key(this.Value);
        }
    }

    internal record Ed25519([property: JsonPropertyName("key")] string Value) : PublicKey(KeyTypeEnum.Ed25519)
    {
        public override IMessage ToProto()
        {
            return new PubKey
            {
                Key = ByteString.FromBase64(this.Value)
            };
        }

        public override NET.SignatureKey ToModel()
        {
            return new NET.Ed25519Key(this.Value);
        }
    }

    internal record MultisigKey(uint Threshold, [property: JsonConverter(typeof(PublicKeysConverter))] PublicKey[] PublicKeys) : PublicKey(KeyTypeEnum.Multisig)
    {
        public override IMessage ToProto()
        {
            var key = new LegacyAminoPubKey
            {
                Threshold = this.Threshold
            };
            key.PublicKeys.AddRange(this.PublicKeys.Select(key => key.PackAny()).ToArray());
            return key;
        }

        public override NET.SignatureKey ToModel()
        {
            return new NET.MultisigKey(this.Threshold, this.PublicKeys.Select(pk => pk.ToModel()).ToArray());
        }
    }
}
