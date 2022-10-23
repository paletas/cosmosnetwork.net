using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json
{
    [JsonConverter(typeof(PublicKeyConverter))]
    public abstract record PublicKey(KeyTypeEnum Type)
    {
        public abstract SignatureKey ToModel();

        public abstract Proto.PublicKey ToProto();
    }

    internal record Secp256k1([property: JsonPropertyName("key")] string Value) : PublicKey(KeyTypeEnum.Secp256k1)
    {
        public override Proto.PublicKey ToProto()
        {
            return new Proto.PublicKey
            {
                Secp256k1 = Convert.FromBase64String(this.Value)
            };
        }

        public override SignatureKey ToModel()
        {
            return new Secp256k1Key(this.Value);
        }
    }

    internal record Ed25519([property: JsonPropertyName("key")] string Value) : PublicKey(KeyTypeEnum.Ed25519)
    {
        public override Proto.PublicKey ToProto()
        {
            return new Proto.PublicKey
            {
                Ed25519 = Convert.FromBase64String(this.Value)
            };
        }

        public override SignatureKey ToModel()
        {
            return new Ed25519Key(this.Value);
        }
    }

    internal record MultisigKey(uint Threshold, [property: JsonConverter(typeof(PublicKeysConverter))] PublicKey[] PublicKeys) : PublicKey(KeyTypeEnum.Multisig)
    {
        public override Proto.PublicKey ToProto()
        {
            throw new NotImplementedException();
        }

        public override SignatureKey ToModel()
        {
            return new CosmosNetwork.MultisigKey(this.Threshold, this.PublicKeys.Select(pk => pk.ToModel()).ToArray());
        }
    }
}
