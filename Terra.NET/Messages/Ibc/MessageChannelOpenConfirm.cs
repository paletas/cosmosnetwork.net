namespace Terra.NET.Messages.Ibc
{
    internal record MessageChannelOpenConfirm()
        : Message(MessageTypeEnum.IbcChannelOpenConfirm)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageChannelOpenConfirm();
        }
    } 
}
