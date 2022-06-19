namespace Terra.NET.Messages.Ibc
{
    internal record MessageChannelOpenAcknowledgement()
        : Message(MessageTypeEnum.IbcChannelOpenAcknowledgement)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageChannelOpenAcknowledgement();
        }
    } 
}
