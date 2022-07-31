using ProtoBuf;

namespace CosmosNetwork.Serialization.Tendermint
{
    [ProtoContract]
    internal class PublicKey : IHasAny
    {
        private ProtoBuf.DiscriminatedUnionObject _discriminatedObject = new();

        public string TypeUrl => "/tendermint.crypto.PublicKey";

        public PublicKeyDiscriminator Discriminator => (PublicKeyDiscriminator)_discriminatedObject.Discriminator;

        [ProtoMember(2, Name = "ed25519")]
        public byte[]? Ed25519
        {
            get
            {
                if (this.Discriminator == PublicKeyDiscriminator.Ed25519)
                {
                    return this._discriminatedObject.Object as byte[];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                _discriminatedObject = new DiscriminatedUnionObject((int)ModeInfoDiscriminatorEnum.Single, value);
            }
        }

        [ProtoMember(3, Name = "secp256k1")]
        public byte[]? Secp256k1
        {
            get
            {
                if (this.Discriminator == PublicKeyDiscriminator.Secp256k1)
                {
                    return this._discriminatedObject.Object as byte[];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                _discriminatedObject = new DiscriminatedUnionObject((int)ModeInfoDiscriminatorEnum.Multi, value);
            }
        }

        public CosmosNetwork.SignatureKey ToModel()
        {
            return this.Discriminator switch
            {
                PublicKeyDiscriminator.Ed25519 => new CosmosNetwork.Ed25519Key(Convert.ToBase64String(this.Ed25519!)),
                PublicKeyDiscriminator.Secp256k1 => new CosmosNetwork.Ed25519Key(Convert.ToBase64String(this.Secp256k1!)),
            };
        }
    }

    internal enum PublicKeyDiscriminator
    {
        Ed25519 = 2,
        Secp256k1 = 3
    }
}