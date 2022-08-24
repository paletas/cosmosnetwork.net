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
            switch (this.Type)
            {
                case PublicKeyEnum.Ed25519:
                    return new Serialization.Crypto.PublicKey { Ed25519 = this.Data };
                case PublicKeyEnum.Secp256k1:
                    return new Serialization.Crypto.PublicKey { Secp256k1 = this.Data };
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public enum PublicKeyEnum
    {
        Ed25519 = 2,
        Secp256k1 = 3
    }
}