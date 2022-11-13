using CosmosNetwork.Keys;
using ProtoBuf;

namespace CosmosNetwork.Serialization.Proto
{
    [ProtoContract]
    [ProtoInclude(1, typeof(Secp256k1))]
    [ProtoInclude(2, typeof(Ed25519))]
    [ProtoInclude(3, typeof(MultisigKey))]
    public abstract record PublicKey([property: ProtoIgnore] string TypeUrl) : IHasAny
    {
        public abstract IPublicKey ToModel();
    }

    [ProtoContract]
    internal record Secp256k1(
        [property: ProtoMember(1, Name = "key")] string Value) : PublicKey("/cosmos.crypto.secp256k1.PubKey")
    {
        public override IPublicKey ToModel()
        {
            return new BasicPublicKey(this.Value, BasicPublicKey.CurveAlgorithm.Secp256k1);
        }
    }

    [ProtoContract]
    internal record Ed25519(
        [property: ProtoMember(1, Name = "key")] string Value) : PublicKey("/cosmos.crypto.ed25519.PubKey")
    {
        public override IPublicKey ToModel()
        {
            return new BasicPublicKey(this.Value, BasicPublicKey.CurveAlgorithm.Ed25519);
        }
    }

    [ProtoContract]
    internal record MultisigKey(
        [property: ProtoMember(1, Name = "threshold")] uint Threshold,
        [property: ProtoMember(2, Name = "public_keys")] PublicKey[] PublicKeys) : PublicKey("/cosmos.crypto.multisig.LegacyAminoPubKey")
    {
        public override IPublicKey ToModel()
        {
            return new MultisigPublicKey(this.Threshold, this.PublicKeys.Select(key => key.ToModel()).ToArray());
        }
    }
}
