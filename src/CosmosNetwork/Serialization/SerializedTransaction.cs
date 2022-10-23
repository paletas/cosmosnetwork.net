namespace CosmosNetwork.Serialization
{
    internal record SerializedTransaction(byte[] Payload, Fee? Fees = null) : ITransactionPayload
    {
        public string GetBase64()
        {
            return Convert.ToBase64String(this.Payload);
        }

        public byte[] GetBytes()
        {
            return this.Payload;
        }

        public CosmosNetwork.SerializedTransaction ToModel()
        {
            return new CosmosNetwork.SerializedTransaction(this.Payload, this.Fees?.ToModel());
        }
    }
}
