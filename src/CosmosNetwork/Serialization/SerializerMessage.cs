using CosmosNetwork.Serialization.Proto;
using ProtoBuf;

namespace CosmosNetwork.Serialization
{
    public abstract record SerializerMessage([property: ProtoIgnore] string TypeUrl) : IHasAny
    {
        protected internal abstract CosmosNetwork.Message ToModel();
    };
}
