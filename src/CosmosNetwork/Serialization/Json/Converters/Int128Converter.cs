using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
  internal class Int128Converter : JsonConverter<Int128>
    {
        public override Int128 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? extraLong = reader.GetString();
            return extraLong is null
                ? throw new JsonException()
                : extraLong.Equals("0", StringComparison.InvariantCulture)
                ? Int128.Zero
                : Int128.TryParse(extraLong, out Int128 int128) ? int128 : throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Int128 value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
