using Google.Protobuf;
using System.Text.Json;

namespace Terra.NET.API.Serialization.Json.Messages.Ibc
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageConnectionOpenAcknowledgement()
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "ibc/MsgConnectionOpenAck";
        public const string COSMOS_DESCRIPTOR = "/ibc.core.connection.v1.MsgConnectionOpenAck";

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            throw new NotImplementedException();
        }

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Ibc.MessageConnectionOpenAcknowledgement();
        }
    }
}
