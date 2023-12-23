namespace CosmosNetwork
{
    public interface IKey
    {
        byte[] RawKey { get; }
    }

    public interface IKeySource
    {
        IPrivateKey PrivateKey { get; }

        IPublicKey PublicKey { get; }
    }

    public interface IPrivateKey : IKey
    {
        Task<byte[]> SignPayload(byte[] payload, CancellationToken cancellationToken = default);

        Task<byte[]> SignTransaction(string chainId, ulong accountNumber, Transaction transaction, CancellationToken cancellationToken = default);
    }

    public interface IPublicKey : IKey
    {
        string AsAddress(string prefix);

        Serialization.Proto.PublicKey ToProto();

        Serialization.Json.PublicKey ToJson();
    }
}