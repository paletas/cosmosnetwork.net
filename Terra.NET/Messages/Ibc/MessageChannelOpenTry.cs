namespace Terra.NET.Messages.Ibc
{
    internal record MessageChannelOpenTry()
        : Message(MessageTypeEnum.IbcChannelOpenTry)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageChannelOpenTry();
        }
    } 
}
