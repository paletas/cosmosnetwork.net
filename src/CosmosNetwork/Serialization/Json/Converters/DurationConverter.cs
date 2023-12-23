using ProtoBuf.WellKnownTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
    internal class DurationConverter : JsonConverter<Duration>
    {
        public override Duration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string? value = reader.GetString();
            if (value is null)
            {
                throw new JsonException();
            }

            char unit = value[^1];

            return unit switch
            {
                's' => new Duration(long.Parse(value[..1]), 0),
                _ => throw new NotSupportedException($"unit {unit} is not supported in Duration deserialization"),
            };
        }

        public override void Write(Utf8JsonWriter writer, Duration value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
