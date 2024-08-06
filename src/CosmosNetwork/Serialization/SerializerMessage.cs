using CosmosNetwork.Serialization.Proto;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization
{
    public abstract record SerializerMessage([property: ProtoIgnore, JsonPropertyName("@type")] string TypeUrl) : IHasAny
    {
        public abstract CosmosNetwork.Message ToModel();
    };
}
