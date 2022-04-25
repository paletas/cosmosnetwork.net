using Google.Protobuf;
using System.Text.Json;

namespace Terra.NET.API.Serialization.Json
{
    internal abstract record Message(string MessageType, string CosmosMessageType)
    {
        internal abstract IMessage ToProto(JsonSerializerOptions? serializerOptions = null);

        internal Google.Protobuf.WellKnownTypes.Any PackAny(JsonSerializerOptions? serializerOptions = null)
        {
            var anyPack = new Google.Protobuf.WellKnownTypes.Any
            {
                TypeUrl = MessageType,
                Value = ToProto(serializerOptions).ToByteString()
            };
            return anyPack;
        }

        internal abstract Terra.NET.Message ToModel();
    };
}
