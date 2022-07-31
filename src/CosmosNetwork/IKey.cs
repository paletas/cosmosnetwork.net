namespace CosmosNetwork
{
    internal interface IKey
    {
        PublicKey PublicKey { get; }

        Serialization.Transaction SignTransaction(Transaction transaction, SignerOptions[] signers, SignOptions signOptions);
    }
}
