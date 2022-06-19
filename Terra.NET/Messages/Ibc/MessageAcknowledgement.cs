namespace Terra.NET.Messages.Ibc
{
    internal record MessageAcknowledgement()
        : Message(MessageTypeEnum.IbcAcknowledgement)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageAcknowledgement();
        }
    } 
}
