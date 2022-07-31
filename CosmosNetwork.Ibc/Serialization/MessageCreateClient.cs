using CosmosNetwork;

namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageCreateClient()
        : Message(MessageTypeEnum.IbcCreateClient)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageCreateClient();
        }
    }
}
