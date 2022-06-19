namespace Terra.NET.Messages.Ibc
{
    internal record MessageCreateClient()
        : Message(MessageTypeEnum.IbcCreateClient)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageCreateClient();
        }
    } 
}
