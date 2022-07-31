using CosmosNetwork;

namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageConnectionOpenAcknowledgement()
        : Message(MessageTypeEnum.IbcConnectionOpenAcknowledgement)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageConnectionOpenAcknowledgement();
        }
    }
}
