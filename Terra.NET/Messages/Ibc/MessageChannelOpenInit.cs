namespace Terra.NET.Messages.Ibc
{
    internal record MessageChannelOpenInit()
        : Message(MessageTypeEnum.IbcChannelOpenInit)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageChannelOpenInit();
        }
    } 
}
