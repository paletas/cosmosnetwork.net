namespace Terra.NET
{
    public record SignerOptions(string AccountNumber, ulong Sequence, PublicKey PublicKey);

    public record SignOptions(string ChainId);

    public record Signature(PublicKey PublicKey, SignatureDescriptor Data, ulong Sequence);

    public record SignatureDescriptor(SignerModeEnum Mode, string Signature);

    public enum SignerModeEnum
    {
        Direct,
        Textual,
        AminoLegacy
    }
}
