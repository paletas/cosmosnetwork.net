using CosmosNetwork.Ibc.Serialization.LightClients;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Json
{
    internal class HeaderConverter : JsonConverter<IHeader>
    {
        public override IHeader? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader innerReader = reader; //local copy

            if (innerReader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (!innerReader.Read() || innerReader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            if (!innerReader.Read() || innerReader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string? type = innerReader.GetString();
            IHeader? returnObj = type switch
            {
                "/ibc.lightclients.solomachine.v1.Header" => JsonSerializer.Deserialize<LightClients.SoloMachine.HeaderV1>(ref reader, options),
                "/ibc.lightclients.solomachine.v2.Header" => JsonSerializer.Deserialize<LightClients.SoloMachine.Header>(ref reader, options),
                "/ibc.lightclients.tendermint.v1.Header" => JsonSerializer.Deserialize<LightClients.Tendermint.Header>(ref reader, options),
                _ => throw new JsonException($"type {type} not supported"),
            };
            return returnObj;
        }

        public override void Write(Utf8JsonWriter writer, IHeader value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
