using CosmosNetwork;
using Google.Protobuf;
using System.Text.Json;

namespace CosmosNetwork.Serialization.Messages.Ibc
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageChannelOpenInit()
        : SerializerMessage(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "ibc/MsgChannelOpenInit";
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgChannelOpenInit";

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            throw new NotImplementedException();
        }

        internal override SerializerMessage ToModel()
        {
            return new NET.Messages.Ibc.MessageChannelOpenInit();
        }
    }
}
