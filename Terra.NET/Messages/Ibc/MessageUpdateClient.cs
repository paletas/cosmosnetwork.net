namespace Terra.NET.Messages.Ibc
{
    internal record MessageUpdateClient()
        : Message(MessageTypeEnum.IbcUpdateClient)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageUpdateClient();
        }
    } 
}
