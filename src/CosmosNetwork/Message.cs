using System.Reflection;

namespace CosmosNetwork
{
    public abstract record Message(string MessageType)
    {
        public abstract Serialization.SerializerMessage ToSerialization();
    }
}