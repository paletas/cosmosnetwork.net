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
                messages.Add(JsonSerializer.Deserialize<Message>(ref reader, serializerOptions));
            }
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray);
            return messages.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, Message[] value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    internal class MessageConverter : JsonConverter<Message>
    {
        public override bool CanConvert(Type type)
        {
            return typeof(Message).IsAssignableFrom(type);
        }

        public override Message Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader innerReader = reader;  //This allows us to read some here and leave the original at the start

            if (innerReader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (!innerReader.Read() || innerReader.TokenType != JsonTokenType.PropertyName || innerReader.GetString() != "@type")
            {
                throw new JsonException();
            }

            if (!innerReader.Read() || innerReader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string typeDiscriminator = innerReader.GetString() ?? throw new NotSupportedException();
            Message baseClass = typeDiscriminator switch
            {
                MessageSend.MESSAGETYPE_DESCRIPTOR => ((MessageSend?)JsonSerializer.Deserialize(ref reader, typeof(MessageSend))) ?? throw new JsonException(),
                MessageSwap.MESSAGETYPE_DESCRIPTOR => ((MessageSwap?)JsonSerializer.Deserialize(ref reader, typeof(MessageSwap))) ?? throw new JsonException(),
                MessageExecuteContract.MESSAGETYPE_DESCRIPTOR => ((MessageExecuteContract?)JsonSerializer.Deserialize(ref reader, typeof(MessageExecuteContract))) ?? throw new JsonException(),
                _ => throw new NotSupportedException(),
            };

            return baseClass;
        }

        public override void Write(Utf8JsonWriter writer, Message value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value is MessageSend messageSend)
            {
                writer.WriteString("@type", MessageSend.MESSAGETYPE_DESCRIPTOR);
                JsonSerializer.Serialize(writer, messageSend);
            }
            else if (value is MessageSwap messageSwap)
            {
                writer.WriteString("@type", MessageSwap.MESSAGETYPE_DESCRIPTOR);
                JsonSerializer.Serialize(writer, messageSwap);
            }
            else if (value is MessageExecuteContract messageExecuteContract)
            {
                writer.WriteString("@type", MessageExecuteContract.MESSAGETYPE_DESCRIPTOR);
                JsonSerializer.Serialize(writer, messageExecuteContract);
            }
            else
            {
                throw new NotSupportedException();
            }

            writer.WriteEndObject();
        }
    }
}
