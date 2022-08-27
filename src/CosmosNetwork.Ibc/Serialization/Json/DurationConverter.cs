using ProtoBuf.WellKnownTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Json
{
    internal class DurationConverter : JsonConverter<Duration>
    {
        public override Duration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String) throw new JsonException();

            var value = reader.GetString();
            if (value is null) throw new JsonException();

            Char unit = value[value.Length - 1];

            switch (unit)
            {
                case 's':
                    return new Duration(long.Parse(value[..1]), 0);
                default:
                    throw new NotSupportedException($"unit {unit} is not supported in Duration deserialization");
            }
        }

        public override void Write(Utf8JsonWriter writer, Duration value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
