namespace Terra.NET
{
    public record SignerOptions(string AccountNumber, ulong Sequence, PublicKey PublicKey);

    public record SignOptions(string ChainId);

    public record TransactionSigner(PublicKey PublicKey, SignatureDescriptor Data, ulong Sequence)
        : TransactionSignature(PublicKey, Data.Signature);

    public record TransactionSignature(PublicKey? PublicKey, string Signature);

    public record SignatureDescriptor(SignerModeEnum Mode, string Signature);

    public enum SignerModeEnum
    {
        Direct,
        Textual,
        AminoLegacy
    }
}
