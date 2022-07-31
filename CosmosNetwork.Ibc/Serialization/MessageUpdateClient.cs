namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageUpdateClient()
        : Message(MessageTypeEnum.IbcUpdateClient)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageUpdateClient();
        }
    }
}
