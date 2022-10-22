namespace CosmosNetwork.Serialization
{
    internal record SerializedTransaction(byte[] Payload, Fee? Fees = null) : ITransactionPayload
    {
        public string GetBase64()
        {
            return Convert.ToBase64String(Payload);
        }

        public byte[] GetBytes()
        {
            return Payload;
        }

        public CosmosNetwork.SerializedTransaction ToModel()
        {
            return new CosmosNetwork.SerializedTransaction(Payload, Fees?.ToModel());
        }
    }
}
