using ProtoBuf;

namespace CosmosNetwork.Tendermint.Types.Serialization.Crypto
{
    [ProtoContract]
    public class PublicKey
    {
        private ProtoBuf.DiscriminatedUnionObject _discriminatedObject = new();

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

        public Types.Crypto.PublicKey ToModel()
        {
            return this.Discriminator switch
            {
                PublicKeyDiscriminator.Ed25519 => new Types.Crypto.PublicKey(Types.Crypto.PublicKeyEnum.Ed25519, this.Ed25519 ?? throw new InvalidOperationException()),
                PublicKeyDiscriminator.Secp256k1 => new Types.Crypto.PublicKey(Types.Crypto.PublicKeyEnum.Secp256k1, this.Secp256k1 ?? throw new InvalidOperationException()),
                _ => throw new NotSupportedException(),
            };
        }
    }

    public enum PublicKeyDiscriminator
    {
        Ed25519 = 2,
        Secp256k1 = 3
    }
}