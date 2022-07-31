using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization
{
    internal abstract record PublicKey(KeyTypeEnum Type)
    {
        public abstract SignatureKey ToModel();

        public abstract Tendermint.PublicKey AsTendermint();
    }

    internal record Secp256k1([property: JsonPropertyName("key")] string Value) : PublicKey(KeyTypeEnum.Secp256k1)
    {
        public override Tendermint.PublicKey AsTendermint()
        {
            return new Tendermint.PublicKey
            {
                Secp256k1 = Convert.FromBase64String(this.Value)
            };
        }

        public override SignatureKey ToModel()
        {
            return new CosmosNetwork.Secp256k1Key(Value);
        }
    }

    internal record Ed25519([property: JsonPropertyName("key")] string Value) : PublicKey(KeyTypeEnum.Ed25519)
    {
        public override Tendermint.PublicKey AsTendermint()
        {
            return new Tendermint.PublicKey
            {
                Ed25519 = Convert.FromBase64String(this.Value)
            };
        }

        public override SignatureKey ToModel()
        {
            return new CosmosNetwork.Ed25519Key(Value);
        }
    }

    internal record MultisigKey(uint Threshold, [property: JsonConverter(typeof(PublicKeysConverter))] PublicKey[] PublicKeys) : PublicKey(KeyTypeEnum.Multisig)
    {
        public override Tendermint.PublicKey AsTendermint()
        {
            throw new NotImplementedException();
        }

        public override SignatureKey ToModel()
        {
            return new CosmosNetwork.MultisigKey(Threshold, PublicKeys.Select(pk => pk.ToModel()).ToArray());
        }
    }
}
