namespace Terra.NET.Messages.Ibc
{
    internal record MessageConnectionOpenConfirm()
        : Message(MessageTypeEnum.IbcConnectionOpenConfirm)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageConnectionOpenConfirm();
        }
    } 
}
