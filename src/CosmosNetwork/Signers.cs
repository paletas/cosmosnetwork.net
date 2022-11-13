namespace CosmosNetwork
{
    public record SignOptions(string ChainId);

    public record TransactionSignature(IPublicKey Key, string Signature);

    public record TransactionSigner(IPublicKey Key, SignatureDescriptor Data, ulong Sequence)
        : TransactionSignature(Key, Data.Signature);

    public record SignatureDescriptor(SignerModeEnum Mode, string Signature);

    public enum SignerModeEnum
    {
        Direct,
        Textual,
        AminoLegacy
    }
}
