using CosmosNetwork.Serialization;

namespace CosmosNetwork
{
    public class CosmosMessageRegistry
    {
        private readonly IDictionary<string, (Type Message, Type SerializerMessage)> _messages
            = new Dictionary<string, (Type Message, Type SerializerMessage)>();

        public static CosmosMessageRegistry Instance { get; private set; } = null!;

        public CosmosMessageRegistry()
        {
            Instance = this;
        }

        public void RegisterMessage<TM, TS>(string typeDescriptor)
            where TM : Message
            where TS : SerializerMessage
        {
            RegisterMessage(typeof(TM), typeof(TS), typeDescriptor);
        }

        public void RegisterMessage(Type messageType, Type serializerType, string typeDescriptor)
        {
            this._messages.Add(typeDescriptor, (messageType, serializerType));
        }

        internal Type? GetMessageType(string type)
        {
            return this._messages.TryGetValue(type, out (Type Message, Type SerializerMessage) value) ? value.Message : null;
        }

        internal Type? GetSerializerMessageType(string type)
        {
            return this._messages.TryGetValue(type, out (Type Message, Type SerializerMessage) value) ? value.SerializerMessage : null;
        }

        internal string GetMessageTypeName(Type type)
        {
            return this._messages.Single(kv => kv.Value.Message == type).Key;
        }

        internal string GetSerializerMessageTypeName(Type type)
        {
            return this._messages.Single(kv => kv.Value.SerializerMessage == type).Key;
        }
    }
}
