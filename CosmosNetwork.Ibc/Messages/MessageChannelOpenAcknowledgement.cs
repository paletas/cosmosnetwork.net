using Google.Protobuf;
using System.Text.Json;

namespace CosmosNetwork.Serialization.Messages.Ibc
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageChannelOpenAcknowledgement()
        : SerializerMessage(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "ibc/MsgChannelOpenAck";
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgChannelOpenAck";

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            throw new NotImplementedException();
        }

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Ibc.MessageChannelOpenAcknowledgement();
        }
    }
}
