using CosmosNetwork;

namespace CosmosNetwork.Messages.Ibc
{
    internal record MessageChannelOpenConfirm()
        : Message(MessageTypeEnum.IbcChannelOpenConfirm)
    {
        internal override Serialization.SerializerMessage ToJson()
        {
            return new API.Serialization.Json.Messages.Ibc.MessageChannelOpenConfirm();
        }
    }
}
