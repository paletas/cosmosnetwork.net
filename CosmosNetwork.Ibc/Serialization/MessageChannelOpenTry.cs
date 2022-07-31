namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageChannelOpenTry()
        : Message(MessageTypeEnum.IbcChannelOpenTry)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageChannelOpenTry();
        }
    }
}
