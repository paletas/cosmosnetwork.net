using ProtoBuf;

namespace CosmosNetwork.Serialization
{
    [ProtoContract]
    internal record Transaction(
        [property: ProtoMember(1, Name = "body")] TransactionBody Body,
        [property: ProtoMember(2, Name = "auth_info")] AuthInfo AuthInfo,
        [property: ProtoMember(3, Name = "messages")] List<byte[]> Signatures) : ITransactionPayload
    {
        public byte[] GetBytes()
        {
            return AsBytes(this);
        }

        public string GetBase64()
        {
            return Convert.ToBase64String(AsBytes(this));
        }

        private static byte[] AsBytes<T>(T instance)
        {
            using MemoryStream memoryStream = new();
            Serializer.Serialize<T>(memoryStream, instance);
            return memoryStream.ToArray();
        }
    }

    [ProtoContract]
    internal record TransactionBody(
        [property: ProtoMember(1, Name = "messages")] SerializerMessage[] Messages,
        [property: ProtoMember(3, Name = "amount")] ulong? TimeoutHeight,
        [property: ProtoMember(2, Name = "memo")] string? Memo)
    {

    }

    [ProtoContract]
    internal record AuthInfo(
        [property: ProtoMember(1, Name = "signer_infos")] List<SignatureDescriptor> Signers,
        [property: ProtoMember(2, Name = "fee")] Fee? Fee)
    {

    }

    [ProtoContract]
    internal record Fee(
        [property: ProtoMember(1, Name = "amount")] DenomAmount[] Amount,
        [property: ProtoMember(2, Name = "gas_limit")] ulong GasLimit,
        [property: ProtoMember(3, Name = "payer")] string? PayerAddress,
        [property: ProtoMember(4, Name = "granter")] string? GranterAddress)
    {
        public CosmosNetwork.Fee ToModel()
        {
            return new CosmosNetwork.Fee(
                this.GasLimit, 
                this.Amount.Select(c => c.ToModel()).ToArray(), 
                this.PayerAddress is null ? null : (CosmosAddress) this.PayerAddress,  
                this.GranterAddress is null ? null : (CosmosAddress) this.GranterAddress);
        }
    }

    internal record SignDoc(
        [property: ProtoMember(1, Name = "body_bytes")] TransactionBody Body,
        [property: ProtoMember(2, Name = "auth_info_bytes")] AuthInfo AuthInfo,
        [property: ProtoMember(3, Name = "chain_id")] string ChainId,
        [property: ProtoMember(4, Name = "account_number")] ulong AccountNumber)
    {
        public byte[] BodyBytes => AsBytes(Body);

        public byte[] AuthInfoBytes => AsBytes(AuthInfo);

        public byte[] ToByteArray()
        {
            return AsBytes(this);
        }

        private static byte[] AsBytes<T>(T instance)
        {
            using MemoryStream memoryStream = new();
            Serializer.Serialize<T>(memoryStream, instance);
            return memoryStream.ToArray();
        }
    }

    internal record SerializedTransaction(byte[] Payload, Fee Fees) : ITransactionPayload
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
            return new CosmosNetwork.SerializedTransaction(this.Payload, this.Fees.ToModel());
        }
    }
}
