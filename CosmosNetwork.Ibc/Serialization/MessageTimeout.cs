using CosmosNetwork;

namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageTimeout()
        : Message(MessageTypeEnum.IbcTimeout)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageTimeout();
        }
    }
}
