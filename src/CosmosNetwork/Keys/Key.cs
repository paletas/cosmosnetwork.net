namespace CosmosNetwork.Keys
{
    internal abstract class Key : IKey
    {
        protected byte[] PrivateKey { get; private set; } = null!;

        public PublicKey PublicKey { get; private set; } = null!;

        protected void SetKeys(byte[] privateKey, PublicKey publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        public abstract Task<byte[]> SignPayload(byte[] payload, CancellationToken cancellationToken = default);

        public abstract Task<byte[]> SignTransaction(string chainId, ulong accountNumber, Transaction transaction, CancellationToken cancellationToken = default);
    }
}
