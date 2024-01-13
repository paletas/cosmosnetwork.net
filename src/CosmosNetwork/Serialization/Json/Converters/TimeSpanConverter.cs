using Nethereum.ABI.Util;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
    internal class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string? value = reader.GetString() ?? throw new JsonException();
            string unit = IsNumber(value[^2]) ? value[^1..] : value[^2..];

            return unit switch
            {
                "ms" => TimeSpan.FromMilliseconds(double.Parse(value[..^1])),
                "s" => TimeSpan.FromSeconds(double.Parse(value[..^1])),
                "h" => TimeSpan.FromHours(double.Parse(value[..^1])),
                _ => throw new NotSupportedException($"unit {unit} is not supported in TimeSpan deserialization"),
            };
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsNumber(char c)
        {
            return c >= '0' && c <= '9';
        }
    }
}
