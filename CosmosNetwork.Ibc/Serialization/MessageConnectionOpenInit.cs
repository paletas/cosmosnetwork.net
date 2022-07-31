using CosmosNetwork;

namespace CosmosNetwork.Messages.Ibc
{

    public record MessageConnectionOpenInit()
        : Message(MessageTypeEnum.IbcConnectionOpenInit)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageConnectionOpenInit();
        }
    }
}
