using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
    public class DenomAmountConverter : JsonConverter<DenomAmount>
    {
        public override DenomAmount? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string? value = reader.GetString() ?? throw new JsonException();

            int assetIdx = 0;
            for (; char.IsLetter(value[assetIdx]) == false; ++assetIdx) ;

            return new DenomAmount(value[assetIdx..], value[..assetIdx]);
        }

        public override void Write(Utf8JsonWriter writer, DenomAmount value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
