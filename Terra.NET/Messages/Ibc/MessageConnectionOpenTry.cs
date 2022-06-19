namespace Terra.NET.Messages.Ibc
{
    internal record MessageConnectionOpenTry()
        : Message(MessageTypeEnum.IbcConnectionOpenTry)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageConnectionOpenTry();
        }
    } 
}
