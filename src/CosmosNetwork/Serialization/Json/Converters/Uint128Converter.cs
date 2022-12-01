using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
  internal class Uint128Converter : JsonConverter<UInt128>
    {
        public override UInt128 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? extraLong = reader.GetString();
            return extraLong is null
                ? throw new JsonException()
                : extraLong.Equals("0", StringComparison.InvariantCulture)
                ? UInt128.Zero
                : UInt128.TryParse(extraLong, out UInt128 int128) ? int128 : throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, UInt128 value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
