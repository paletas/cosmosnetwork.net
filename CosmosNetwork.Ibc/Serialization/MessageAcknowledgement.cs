using CosmosNetwork;

namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageAcknowledgement()
        : Message(MessageTypeEnum.IbcAcknowledgement)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageAcknowledgement();
        }
    }
}
