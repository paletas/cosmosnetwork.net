using ProtoBuf.WellKnownTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
    internal class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String) throw new JsonException();

            var value = reader.GetString();
            if (value is null) throw new JsonException();

            char unit = value[value.Length - 1];

            return unit switch
            {
                's' => TimeSpan.FromSeconds(double.Parse(value[..1])),
                _ => throw new NotSupportedException($"unit {unit} is not supported in TimeSpan deserialization"),
            };
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
