namespace CosmosNetwork
{
    public class CosmosMessageRegistry
    {
        private readonly IDictionary<string, Type> _messages
            = new Dictionary<string, Type>();

        public void RegisterMessages(System.Reflection.Assembly assembly)
        {
            var messages = assembly.GetTypes().Where(t => typeof(Message).IsAssignableFrom(t)).ToArray();

            foreach (var message in messages) RegisterMessage(message);
        }

        public void RegisterMessage<TM>()
            where TM : Message
        {
            RegisterMessage(typeof(TM));
        }

        public void RegisterMessage(Type messageType)
        {
            var messageAttr = GetMessageDescriptor(messageType);
            if (messageAttr is null)
                throw new InvalidOperationException();

            _messages.Add(messageAttr.CosmosType, messageType);
            if (messageAttr.CustomTypeAlias is not null)
                _messages.Add(messageAttr.CustomTypeAlias, messageType);
        }

        public void ReplaceMessage<TO, TN>()
            where TO : Message
            where TN : Message
        {
            var previousMessageAttr = GetMessageDescriptor<TO>();
            var messageAttr = GetMessageDescriptor<TO>();

            if (previousMessageAttr is null)
                throw new InvalidOperationException();

            if (messageAttr is null)
                throw new InvalidOperationException();

            if (previousMessageAttr.CosmosType != messageAttr.CosmosType)
                throw new InvalidOperationException();

            _messages[previousMessageAttr.CosmosType] = typeof(TN);
            if (messageAttr.CustomTypeAlias is not null)
            {
                if (_messages.ContainsKey(messageAttr.CustomTypeAlias))
                {
                    _messages[messageAttr.CustomTypeAlias] = typeof(TN);
                }
                else
                {
                    _messages.Add(messageAttr.CustomTypeAlias, typeof(TN));
                }
            }
        }

        internal Type? GetMessageType(string type)
        {
            if (_messages.ContainsKey(type))
                return _messages[type];
            else
                return null;
        }

        internal string GetMessageTypeName(Type type)
        {
            return _messages.Single(kv => kv.Value == type).Key;
        }

        private CosmosMessageAttribute? GetMessageDescriptor<TM>() where TM : Message
        {
            return GetMessageDescriptor(typeof(TM));
        }

        private CosmosMessageAttribute? GetMessageDescriptor(Type messageType)
        {
            return messageType.GetCustomAttributes(false).OfType<CosmosMessageAttribute>().SingleOrDefault();
        }
    }
}
