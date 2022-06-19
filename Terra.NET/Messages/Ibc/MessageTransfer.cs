namespace Terra.NET.Messages.Ibc
{
    internal record MessageTransfer()
        : Message(MessageTypeEnum.IbcTransfer)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageTransfer();
        }
    } 
}
