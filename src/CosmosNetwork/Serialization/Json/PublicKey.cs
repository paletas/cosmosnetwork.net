using CosmosNetwork.Keys;
using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json
{
    [JsonConverter(typeof(PublicKeyConverter))]
    public abstract record PublicKey(KeyTypeEnum Type)
    {
        public abstract IPublicKey ToModel();

        public abstract Proto.PublicKey ToProto();
    }

    internal record Secp256k1([property: JsonPropertyName("key")] string Value) : PublicKey(KeyTypeEnum.Secp256k1)
    {
        public override Proto.PublicKey ToProto()
        {
            return new Proto.Secp256k1(this.Value);
        }

        public override IPublicKey ToModel()
        {
            return new BasicPublicKey(this.Value, BasicPublicKey.CurveAlgorithm.Secp256k1);
        }
    }

    internal record Ed25519([property: JsonPropertyName("key")] string Value) : PublicKey(KeyTypeEnum.Ed25519)
    {
        public override Proto.PublicKey ToProto()
        {
            return new Proto.Ed25519(this.Value);
        }

        public override IPublicKey ToModel()
        {
            return new BasicPublicKey(this.Value, BasicPublicKey.CurveAlgorithm.Ed25519);
        }
    }

    internal record MultisigKey(uint Threshold, [property: JsonConverter(typeof(PublicKeysConverter))] PublicKey[] PublicKeys) : PublicKey(KeyTypeEnum.Multisig)
    {
        public override Proto.PublicKey ToProto()
        {
            return new Proto.MultisigKey(this.Threshold, this.PublicKeys.Select(pk => pk.ToProto()).ToArray());
        }

        public override IPublicKey ToModel()
        {
            return new MultisigPublicKey(this.Threshold, this.PublicKeys.Select(pk => pk.ToModel()).ToArray());
        }
    }
}
