namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageTransfer()
        : Message(MessageTypeEnum.IbcTransfer)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageTransfer();
        }
    }
}
