using CosmosNetwork.Serialization.Proto;
using ProtoBuf;

namespace CosmosNetwork.Serialization
{
    public abstract record SerializerMessage([property: ProtoIgnore] string TypeUrl) : IHasAny
    {
        public abstract CosmosNetwork.Message ToModel();
    };
}
