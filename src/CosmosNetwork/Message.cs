using System.Reflection;

namespace CosmosNetwork
{
    public abstract record Message()
    {
        private string? _messageType;
        public virtual string MessageType => this._messageType ??= GetMessageType();

        protected internal abstract Serialization.SerializerMessage ToSerialization();

        private string GetMessageType()
        {
            CosmosMessageAttribute? attr = GetType().GetCustomAttribute<CosmosMessageAttribute>();
            return attr is null ? throw new InvalidOperationException() : attr.CosmosType;
        }
    }
}