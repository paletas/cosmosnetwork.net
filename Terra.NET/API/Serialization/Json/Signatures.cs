using Cosmos.SDK.Protos.Crypto.Keys.Secp256k1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json
{
    internal record SignerInfo(PublicKey PublicKey, SignerModeInfo ModeInfo);

    internal record SignerModeInfo(SignerSingleMode Single);

    internal record SignerSingleMode(SignerModeEnum Mode);

    internal record SignerOptions([property: JsonPropertyName("pub_key")] PublicKey PublicKey, string Signature)
    {
        public NET.TransactionSignature ToModel()
        {
            var publicKey = this.PublicKey.ToModel();
            return new NET.TransactionSignature(publicKey, this.Signature);
        }
    }

    internal record PublicKey(string Type, string Value)
    {
        public PubKey ToProto()
        {
            return new PubKey
            {
                Key = ByteString.FromBase64(this.Value)
            };
        }

        public Any PackAny()
        {
            return Any.Pack(ToProto(), string.Empty);
        }

        public NET.PublicKey? ToModel()
        {
            if (this.Value == null) return null;
            return new NET.PublicKey(this.Value);
        }
    };
}
