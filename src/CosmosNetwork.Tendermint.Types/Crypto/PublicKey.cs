namespace CosmosNetwork.Tendermint.Types.Crypto
{
    public class PublicKey
    {
        public PublicKey(PublicKeyEnum keyType, byte[] key)
        {
            this.Type = keyType;
            this.Data = key;
        }

        public PublicKeyEnum Type { get; private set; }

        public byte[] Data { get; private set; }

        public Serialization.Crypto.PublicKey ToSerialization()
        {
            return this.Type switch
            {
                PublicKeyEnum.Ed25519 => new Serialization.Crypto.PublicKey { Ed25519 = this.Data },
                PublicKeyEnum.Secp256k1 => new Serialization.Crypto.PublicKey { Secp256k1 = this.Data },
                _ => throw new NotSupportedException(),
            };
        }
    }

    public enum PublicKeyEnum
    {
        Ed25519 = 2,
        Secp256k1 = 3
    }
}