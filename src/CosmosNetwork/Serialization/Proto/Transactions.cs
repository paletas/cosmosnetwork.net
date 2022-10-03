using ProtoBuf;

namespace CosmosNetwork.Serialization.Proto
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
            Serializer.Serialize(memoryStream, instance);
            return memoryStream.ToArray();
        }
    }

    [ProtoContract]
    internal record TransactionBody(
        [property: ProtoMember(1, Name = "messages")] Any[] Messages,
        [property: ProtoMember(3, Name = "amount")] ulong? TimeoutHeight,
        [property: ProtoMember(2, Name = "memo")] string? Memo)
    {
    }

    [ProtoContract]
    internal record AuthInfo(
        [property: ProtoMember(1, Name = "signer_infos")] List<SignatureDescriptor> Signers,
        [property: ProtoMember(2, Name = "fee")] Fee Fee)
    {

    }

    [ProtoContract]
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
            Serializer.Serialize(memoryStream, instance);
            return memoryStream.ToArray();
        }
    }
}
