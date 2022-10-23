namespace CosmosNetwork
{
    public record SignOptions(string ChainId);

    public record TransactionSignature(SignatureKey Key, string Signature);

    public record TransactionSigner(SignatureKey Key, SignatureDescriptor Data, ulong Sequence)
        : TransactionSignature(Key, Data.Signature);

    public record SignatureDescriptor(SignerModeEnum Mode, string Signature);

    public enum SignerModeEnum
    {
        Direct,
        Textual,
        AminoLegacy
    }
}
