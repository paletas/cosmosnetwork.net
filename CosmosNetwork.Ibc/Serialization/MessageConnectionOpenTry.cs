namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageConnectionOpenTry()
        : Message(MessageTypeEnum.IbcConnectionOpenTry)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageConnectionOpenTry();
        }
    }
}
