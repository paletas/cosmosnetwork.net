namespace CosmosNetwork
{
    public record SignOptions(string ChainId);

    public record TransactionSignature(SignatureKey PublicKey, string Signature);

    public record TransactionSigner(SignatureKey PublicKey, SignatureDescriptor Data, ulong Sequence)
        : TransactionSignature(PublicKey, Data.Signature);

    public record SignatureDescriptor(SignerModeEnum Mode, string Signature);

    public enum SignerModeEnum
    {
        Direct,
        Textual,
        AminoLegacy
    }
}
