namespace CosmosNetwork
{
    public interface IKey
    {
        PublicKey PublicKey { get; }

        Task<byte[]> SignPayload(byte[] payload, CancellationToken cancellationToken = default);

        Task<byte[]> SignTransaction(string chainId, ulong accountNumber, Transaction transaction, CancellationToken cancellationToken = default);
    }
}
