using ProtoBuf;

namespace CosmosNetwork.Serialization.Proto
{
    [ProtoContract]
    [ProtoInclude(1, typeof(Secp256k1))]
    [ProtoInclude(2, typeof(Ed25519))]
    [ProtoInclude(3, typeof(MultisigKey))]
    public abstract record SimplePublicKey([property: ProtoIgnore] string TypeUrl) : IHasAny
    {
        public abstract SignatureKey ToModel();

        public abstract Proto.PublicKey ToProto();
    }

    [ProtoContract]
    internal record Secp256k1(
        [property: ProtoMember(1, Name = "key")] string Value) : SimplePublicKey("/cosmos.crypto.secp256k1.PubKey")
    {
        public override Proto.PublicKey ToProto()
        {
            return new Proto.PublicKey
            {
                Secp256k1 = Convert.FromBase64String(Value)
            };
        }

        public override SignatureKey ToModel()
        {
            return new Secp256k1Key(Value);
        }
    }

    [ProtoContract]
    internal record Ed25519(
        [property: ProtoMember(1, Name = "key")] string Value) : SimplePublicKey("/cosmos.crypto.ed25519.PubKey")
    {
        public override Proto.PublicKey ToProto()
        {
            return new Proto.PublicKey
            {
                Ed25519 = Convert.FromBase64String(Value)
            };
        }

        public override SignatureKey ToModel()
        {
            return new Ed25519Key(Value);
        }
    }

    [ProtoContract]
    internal record MultisigKey(
        [property: ProtoMember(1, Name = "threshold")] uint Threshold,
        [property: ProtoMember(2, Name = "public_keys")] SimplePublicKey[] PublicKeys) : SimplePublicKey("/cosmos.crypto.multisig.LegacyAminoPubKey")
    {
        public override Proto.PublicKey ToProto()
        {
            throw new NotImplementedException();
        }

        public override SignatureKey ToModel()
        {
            return new CosmosNetwork.MultisigKey(Threshold, PublicKeys.Select(pk => pk.ToModel()).ToArray());
        }
    }
}
