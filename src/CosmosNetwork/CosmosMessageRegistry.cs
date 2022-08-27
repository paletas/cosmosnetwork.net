using CosmosNetwork.Serialization;

namespace CosmosNetwork
{
    public class CosmosMessageRegistry
    {
        private readonly IDictionary<string, (Type Message, Type SerializerMessage)> _messages
            = new Dictionary<string, (Type Message, Type SerializerMessage)>();

        public void RegisterMessage<TM, TS>()
            where TM : Message
            where TS : SerializerMessage
        {
            RegisterMessage(typeof(TM), typeof(TS));
        }

        public void RegisterMessage(Type messageType, Type serializerType)
        {
            CosmosMessageAttribute? messageAttr = GetMessageDescriptor(messageType, serializerType);
            if (messageAttr is null)
            {
                throw new InvalidOperationException();
            }

            _messages.Add(messageAttr.CosmosType, (messageType, serializerType));
            if (messageAttr.CustomTypeAlias is not null)
            {
                _messages.Add(messageAttr.CustomTypeAlias, (messageType, serializerType));
            }
        }

        internal Type? GetMessageType(string type)
        {
            return _messages.ContainsKey(type) ? _messages[type].Message : null;
        }

        internal Type? GetSerializerMessageType(string type)
        {
            return _messages.ContainsKey(type) ? _messages[type].SerializerMessage : null;
        }

        internal string GetMessageTypeName(Type type)
        {
            return _messages.Single(kv => kv.Value.Message == type).Key;
        }

        internal string GetSerializerMessageTypeName(Type type)
        {
            return _messages.Single(kv => kv.Value.SerializerMessage == type).Key;
        }

        private CosmosMessageAttribute? GetMessageDescriptor<TM, TS>() 
            where TM : Message
            where TS : SerializerMessage
        {
            return GetMessageDescriptor(typeof(TM), typeof(TS));
        }

        private CosmosMessageAttribute? GetMessageDescriptor(Type messageType, Type serializerType)
        {
            return messageType.GetCustomAttributes(false).OfType<CosmosMessageAttribute>().SingleOrDefault();
        }
    }
}
