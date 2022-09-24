namespace CosmosNetwork
{
    public abstract record SignatureKey(SignatureTypeEnum Type)
    {
        public abstract Serialization.Json.PublicKey ToSerialization();

        public abstract PublicKey[] GetPublicKeys();
    }

    public enum SignatureTypeEnum
    {
        Secp256k1,
        Ed25519,
        Multisig
    }

    public record Secp256k1Key(string Key) : SignatureKey(SignatureTypeEnum.Secp256k1)
    {
        public override PublicKey[] GetPublicKeys()
        {
            return new[] { new PublicKey(Key) };
        }

        public override Serialization.Json.PublicKey ToSerialization()
        {
            return new Serialization.Json.Secp256k1(Key);
        }
    }

    public record Ed25519Key(string Key) : SignatureKey(SignatureTypeEnum.Ed25519)
    {
        public override PublicKey[] GetPublicKeys()
        {
            return new[] { new PublicKey(Key) };
        }

        public override Serialization.Json.PublicKey ToSerialization()
        {
            return new Serialization.Json.Ed25519(Key);
        }
    }

    public record MultisigKey(uint Threshold, SignatureKey[] Keys) : SignatureKey(SignatureTypeEnum.Multisig)
    {
        public override PublicKey[] GetPublicKeys()
        {
            return Keys.SelectMany(k => k.GetPublicKeys()).ToArray();
        }

        public override Serialization.Json.PublicKey ToSerialization()
        {
            return new Serialization.Json.MultisigKey(Threshold, Keys.Select(k => k.ToSerialization()).ToArray());
        }
    }
}
