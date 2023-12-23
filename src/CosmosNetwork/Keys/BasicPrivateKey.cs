namespace CosmosNetwork.Keys
{
    internal class BasicPrivateKey : IPrivateKey
    {
        public BasicPrivateKey(byte[] rawKey)
        {
            this.RawKey = rawKey;
        }     

        public byte[] RawKey { get; init; }

        public string AsAddress(string prefix) => Bech32.Encode(prefix, this.RawKey);

        public Task<byte[]> SignPayload(byte[] payload, CancellationToken cancellationToken = default)
        {
            byte[] hash = HashExtensions.SHA256(payload);
            return Task.FromResult(Cryptography.ECDSA.Secp256K1Manager.SignCompact(hash, this.RawKey, out _));
        }

        public Task<byte[]> SignTransaction(string chainId, ulong accountNumber, Transaction transaction, CancellationToken cancellationToken = default)
        {
            return SignPayload(transaction.ToSignatureDocument(chainId, accountNumber).ToByteArray(), cancellationToken);
        }
    }
}
