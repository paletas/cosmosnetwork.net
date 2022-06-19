namespace Terra.NET
{
    public record SignerOptions(string AccountNumber, ulong Sequence, PublicKey PublicKey);

    public record SignOptions(string ChainId);

    public record TransactionSigner(SignatureKey PublicKey, SignatureDescriptor Data, ulong Sequence)
        : TransactionSignature(PublicKey, Data.Signature);

    public record TransactionSignature(SignatureKey PublicKey, string Signature);

    public record SignatureDescriptor(SignerModeEnum Mode, string Signature);

    public abstract record SignatureKey(SignatureTypeEnum Type)
    {
        internal abstract API.Serialization.Json.PublicKey ToJson();

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
            return new[] { new PublicKey(this.Key) }; 
        }

        internal override API.Serialization.Json.PublicKey ToJson()
        {
            return new API.Serialization.Json.Secp256k1(this.Key);
        }
    }

    public record Ed25519Key(string Key) : SignatureKey(SignatureTypeEnum.Ed25519)
    {
        public override PublicKey[] GetPublicKeys()
        {
            return new[] { new PublicKey(this.Key) };
        }

        internal override API.Serialization.Json.PublicKey ToJson()
        {
            return new API.Serialization.Json.Ed25519(this.Key);
        }
    }

    public record MultisigKey(uint Threshold, SignatureKey[] Keys) : SignatureKey(SignatureTypeEnum.Multisig)
    {
        public override PublicKey[] GetPublicKeys()
        {
            return this.Keys.SelectMany(k => k.GetPublicKeys()).ToArray();
        }

        internal override API.Serialization.Json.PublicKey ToJson()
        {
            return new API.Serialization.Json.MultisigKey(this.Threshold, this.Keys.Select(k => k.ToJson()).ToArray());
        }
    }

    public enum SignerModeEnum
    {
        Direct,
        Textual,
        AminoLegacy
    }
}
