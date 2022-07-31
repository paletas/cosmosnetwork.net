using CosmosNetwork;

namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageChannelOpenAcknowledgement()
        : Message(MessageTypeEnum.IbcChannelOpenAcknowledgement)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageChannelOpenAcknowledgement();
        }
    }
}
