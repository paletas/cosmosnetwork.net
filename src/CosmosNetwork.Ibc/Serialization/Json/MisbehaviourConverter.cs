using CosmosNetwork.Ibc.Serialization.LightClients;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Json
{
    internal class MisbehaviourConverter : JsonConverter<IMisbehaviour>
    {
        public override IMisbehaviour? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            IMisbehaviour? returnObj = type switch
            {
                "/ibc.lightclients.solomachine.v1.Misbehaviour" => JsonSerializer.Deserialize<LightClients.SoloMachine.MisbehaviourV1>(ref reader, options),
                "/ibc.lightclients.solomachine.v2.Misbehaviour" => JsonSerializer.Deserialize<LightClients.SoloMachine.Misbehaviour>(ref reader, options),
                "/ibc.lightclients.tendermint.v1.Misbehaviour" => JsonSerializer.Deserialize<LightClients.Tendermint.Misbehaviour>(ref reader, options),
                _ => throw new JsonException($"type {type} not supported"),
            };
            return returnObj;
        }

        public override void Write(Utf8JsonWriter writer, IMisbehaviour value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
