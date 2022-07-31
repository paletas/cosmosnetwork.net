using Google.Protobuf;
using System.Text.Json;

namespace CosmosNetwork.Serialization.Messages.Ibc
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageTransfer()
        : SerializerMessage(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "ibc/MsgTransfer";
        public const string COSMOS_DESCRIPTOR = "/ibc.applications.transfer.v1.MsgTransfer";

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            throw new NotImplementedException();
        }

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Ibc.MessageTransfer();
        }
    }
}
