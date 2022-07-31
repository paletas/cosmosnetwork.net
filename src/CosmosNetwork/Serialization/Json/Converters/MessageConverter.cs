using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
    internal class MessagesConverter : JsonConverter<SerializerMessage[]>
    {
        private readonly CosmosMessageRegistry _messageRegistry;

        public MessagesConverter(CosmosMessageRegistry messageRegistry)
        {
            this._messageRegistry = messageRegistry;
        }

        public override SerializerMessage[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            serializerOptions.Converters.Add(new MessageConverter(_messageRegistry));

            var messages = new List<SerializerMessage>();
            do
            {
                var message = JsonSerializer.Deserialize<SerializerMessage>(ref reader, serializerOptions);
                if (message == null) throw new JsonException();

                messages.Add(message);
            }
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray);

            return messages.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, SerializerMessage[] value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private class MessageConverter : JsonConverter<SerializerMessage>
        {
            private readonly CosmosMessageRegistry _messageRegistry;

            public MessageConverter(CosmosMessageRegistry messageRegistry)
            {
                this._messageRegistry = messageRegistry;
            }

            public override SerializerMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                Utf8JsonReader innerReader = reader;  //This allows us to read some here and leave the original at the start

                if (innerReader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                bool isTerraMode = false;
                if (!innerReader.Read() || innerReader.TokenType != JsonTokenType.PropertyName || innerReader.GetString() != "type" && innerReader.GetString() != "@type")
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

                var messageType = _messageRegistry.GetMessageType(typeDiscriminator) ??
                    throw new JsonException($"message type {typeDiscriminator} not known");

                SerializerMessage baseClass = (SerializerMessage?)JsonSerializer.Deserialize(ref reader, messageType, options) ?? throw new JsonException();

                if (isTerraMode)
                {
                    if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
                    {
                        throw new JsonException();
                    }
                }

                return baseClass;
            }

            public override void Write(Utf8JsonWriter writer, SerializerMessage value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                writer.WriteString("type", _messageRegistry.GetMessageTypeName(value.GetType()));
                writer.WritePropertyName("value");
                JsonSerializer.Serialize(writer, value, options);

                writer.WriteEndObject();
            }
        }
    }
}
