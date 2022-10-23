using ProtoBuf;

namespace CosmosNetwork.Serialization.Proto
{
    [ProtoContract]
    public class PublicKey : IHasAny
    {
        private DiscriminatedUnionObject _discriminatedObject = new();

        [ProtoIgnore]
        public string TypeUrl => "/tendermint.crypto.PublicKey";

        public PublicKeyDiscriminator Discriminator => (PublicKeyDiscriminator)this._discriminatedObject.Discriminator;

        [ProtoMember(2, Name = "ed25519")]
        public byte[]? Ed25519
        {
            get => this.Discriminator == PublicKeyDiscriminator.Ed25519 ? this._discriminatedObject.Object as byte[] : null;

            set => this._discriminatedObject = new DiscriminatedUnionObject((int)PublicKeyDiscriminator.Ed25519, value);
        }

        [ProtoMember(3, Name = "secp256k1")]
        public byte[]? Secp256k1
        {
            get => this.Discriminator == PublicKeyDiscriminator.Secp256k1 ? this._discriminatedObject.Object as byte[] : null;

            set => this._discriminatedObject = new DiscriminatedUnionObject((int)PublicKeyDiscriminator.Secp256k1, value);
        }

        public SignatureKey ToModel()
        {
            return this.Discriminator switch
            {
                PublicKeyDiscriminator.Ed25519 => new Ed25519Key(Convert.ToBase64String(this.Ed25519!)),
                PublicKeyDiscriminator.Secp256k1 => new Ed25519Key(Convert.ToBase64String(this.Secp256k1!)),
                _ => throw new NotImplementedException(),
            };
        }
    }

    public enum PublicKeyDiscriminator
    {
        Ed25519 = 2,
        Secp256k1 = 3
    }
}