namespace Terra.NET
{
    public record SignerOptions(string AccountNumber, ulong Sequence, PublicKey? PublicKey);

    public enum SignerModeEnum
    {
        Direct,
        Textual,
        AminoLegacy
    }
}
