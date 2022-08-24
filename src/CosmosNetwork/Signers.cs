namespace CosmosNetwork
{
    public record SignerOptions(string AccountNumber, ulong Sequence, PublicKey PublicKey);

    public record SignOptions(string ChainId);

    public record TransactionSigner(SignatureKey PublicKey, SignatureDescriptor Data, ulong Sequence)
        : TransactionSignature(PublicKey, Data.Signature);

    public record TransactionSignature(SignatureKey PublicKey, string Signature);

    public record SignatureDescriptor(SignerModeEnum Mode, string Signature);

    public enum SignerModeEnum
    {
        Direct,
        Textual,
        AminoLegacy
    }

    public abstract record SignatureKey(SignatureTypeEnum Type)
    {
        public abstract Serialization.PublicKey ToSerialization();

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

        public override Serialization.PublicKey ToSerialization()
        {
            return new Serialization.Secp256k1(Key);
        }
    }

    public record Ed25519Key(string Key) : SignatureKey(SignatureTypeEnum.Ed25519)
    {
        public override PublicKey[] GetPublicKeys()
        {
            return new[] { new PublicKey(Key) };
        }

        public override Serialization.PublicKey ToSerialization()
        {
            return new Serialization.Ed25519(Key);
        }
    }

    public record MultisigKey(uint Threshold, SignatureKey[] Keys) : SignatureKey(SignatureTypeEnum.Multisig)
    {
        public override PublicKey[] GetPublicKeys()
        {
            return Keys.SelectMany(k => k.GetPublicKeys()).ToArray();
        }

        public override Serialization.PublicKey ToSerialization()
        {
            return new Serialization.MultisigKey(Threshold, Keys.Select(k => k.ToSerialization()).ToArray());
        }
    }
}
