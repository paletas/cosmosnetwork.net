using CosmosNetwork;

namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageReceivePacket()
        : Message(MessageTypeEnum.IbcReceivePacket)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageReceivePacket();
        }
    }
}
