using System.Text.Json;
using System.Text.Json.Serialization;
using UltimateOrb;

namespace CosmosNetwork.Serialization.Json.Converters
{
    internal class Uint128Converter : JsonConverter<UInt128>
    {
        public override UInt128 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? extraLong = reader.GetString();
            if (extraLong is null)
                throw new JsonException();

            if (UInt128.TryParseCStyleNormalizedU128(extraLong, out UInt128 int128))
                return int128;
            else
                throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, UInt128 value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToStringCStyleU128());
        }
    }
}
