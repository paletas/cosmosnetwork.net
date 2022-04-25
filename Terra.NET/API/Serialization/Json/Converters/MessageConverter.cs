using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Converters
{
    internal class MessagesConverter : JsonConverter<Message[]>
    {
        public override Message[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var serializerOptions = new JsonSerializerOptions(options);
            serializerOptions.Converters.Add(new MessageConverter());

            var messages = new List<Message>();
            do
            {
                var message = JsonSerializer.Deserialize<Message>(ref reader, serializerOptions);
                if (message == null) throw new JsonException();

                messages.Add(message);
            }
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray);

            return messages.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, Message[] value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private class MessageConverter : JsonConverter<Message>
        {
            private static readonly IDictionary<string, Type> _TerraMessageTypes
                = new Dictionary<string, Type>();

            private static readonly IDictionary<string, Type> _CosmosMessageTypes
                = new Dictionary<string, Type>();

            static MessageConverter()
            {
                var messageTypes = typeof(MessageConverter).Assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(Message)) && (t.GetCustomAttributes(typeof(MessageDescriptorAttribute), false)?.Length ?? 0) > 0)
                    .Select(t => (MessageDescriptor: (MessageDescriptorAttribute) t.GetCustomAttributes(typeof(MessageDescriptorAttribute), false)[0], MessageType: t));

                foreach (var (messageDescriptor, messageType) in messageTypes)
                {
                    _TerraMessageTypes.TryAdd(messageDescriptor.TerraType, messageType);
                    _CosmosMessageTypes.TryAdd(messageDescriptor.CosmosType, messageType);
                }     
            }

            public override Message Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                Utf8JsonReader innerReader = reader;  //This allows us to read some here and leave the original at the start

                if (innerReader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                bool isTerraMode = false;
                if (!innerReader.Read() || innerReader.TokenType != JsonTokenType.PropertyName || (innerReader.GetString() != "type" && innerReader.GetString() != "@type"))
                {
                    throw new JsonException();
                }
                else
                {
                    isTerraMode = innerReader.GetString() == "type";

                    if (isTerraMode)
                    {
                        //catchup the reader
                        reader.Read(); //@type
                        reader.Read(); //@type value
                    }
                }

                if (!innerReader.Read() || innerReader.TokenType != JsonTokenType.String)
                {
                    throw new JsonException();
                }

                string typeDiscriminator = innerReader.GetString() ?? throw new NotSupportedException();

                if (isTerraMode)
                {
                    if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName || reader.GetString() != "value")
                    {
                        throw new JsonException();
                    }

                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                }

                var messageType = _TerraMessageTypes.ContainsKey(typeDiscriminator) ? _TerraMessageTypes[typeDiscriminator] :
                    _CosmosMessageTypes.ContainsKey(typeDiscriminator) ? _CosmosMessageTypes[typeDiscriminator] :
                    throw new JsonException($"message type {typeDiscriminator} not known");

                Message baseClass = ((Message?)JsonSerializer.Deserialize(ref reader, messageType, options)) ?? throw new JsonException();

                if (isTerraMode)
                {
                    if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
                    {
                        throw new JsonException();
                    }
                }

                return baseClass;
            }

            public override void Write(Utf8JsonWriter writer, Message value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                writer.WriteString("type", value.MessageType);
                writer.WritePropertyName("value");
                JsonSerializer.Serialize(writer, value, options);

                writer.WriteEndObject();
            }
        }
    }
}
