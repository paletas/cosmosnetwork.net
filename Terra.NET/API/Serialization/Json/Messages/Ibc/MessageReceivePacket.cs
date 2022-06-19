using Google.Protobuf;
using System.Text.Json;

namespace Terra.NET.API.Serialization.Json.Messages.Ibc
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageReceivePacket()
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "ibc/MsgRecvPacket";
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgRecvPacket";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Ibc.MessageReceivePacket();
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            throw new NotImplementedException();
        }
    }
}
