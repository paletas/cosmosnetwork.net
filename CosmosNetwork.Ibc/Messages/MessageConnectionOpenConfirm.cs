using CosmosNetwork;
using Google.Protobuf;
using System.Text.Json;

namespace CosmosNetwork.Serialization.Messages.Ibc
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageConnectionOpenConfirm()
        : SerializerMessage(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "ibc/MsgConnectionOpenConfirm";
        public const string COSMOS_DESCRIPTOR = "/ibc.core.connection.v1.MsgConnectionOpenConfirm";

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            throw new NotImplementedException();
        }

        internal override SerializerMessage ToModel()
        {
            return new NET.Messages.Ibc.MessageConnectionOpenConfirm();
        }
    }
}
