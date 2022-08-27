using ProtoBuf.WellKnownTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Json
{
    internal class TimestampConverter : JsonConverter<Timestamp>
    {
        public override Timestamp Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String) throw new JsonException();

            var value = reader.GetString();
            if (value is null) throw new JsonException();

            var timestamp = DateTime.Parse(value);
            return new Timestamp(timestamp);
        }

        public override void Write(Utf8JsonWriter writer, Timestamp value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
