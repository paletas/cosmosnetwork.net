namespace Terra.NET.Messages.Ibc
{
    internal record MessageConnectionOpenAcknowledgement()
        : Message(MessageTypeEnum.IbcConnectionOpenAcknowledgement)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageConnectionOpenAcknowledgement();
        }
    } 
}
