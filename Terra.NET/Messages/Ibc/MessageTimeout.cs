namespace Terra.NET.Messages.Ibc
{
    internal record MessageTimeout()
        : Message(MessageTypeEnum.IbcTimeout)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageTimeout();
        }
    } 
}
