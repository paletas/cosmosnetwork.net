namespace CosmosNetwork
{
    public interface ITransactionPayload
    {
        byte[] GetBytes();

        string GetBase64();
    }
}
