namespace Terra.NET.Messages.Ibc
{

    public record MessageConnectionOpenInit()
        : Message(MessageTypeEnum.IbcConnectionOpenInit)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageConnectionOpenInit();
        }
    }
}
