using CosmosNetwork;

namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageConnectionOpenConfirm()
        : Message(MessageTypeEnum.IbcConnectionOpenConfirm)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageConnectionOpenConfirm();
        }
    }
}
