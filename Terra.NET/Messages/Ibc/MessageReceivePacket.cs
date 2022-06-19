namespace Terra.NET.Messages.Ibc
{
    internal record MessageReceivePacket()
        : Message(MessageTypeEnum.IbcReceivePacket)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageReceivePacket();
        }
    } 
}
