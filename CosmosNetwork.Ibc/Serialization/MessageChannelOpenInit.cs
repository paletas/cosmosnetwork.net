namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageChannelOpenInit()
        : Message(MessageTypeEnum.IbcChannelOpenInit)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageChannelOpenInit();
        }
    }
}
