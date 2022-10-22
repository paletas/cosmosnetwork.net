using CosmosNetwork.Ibc.Serialization.LightClients;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Json
{
    internal class ClientStateConverter : JsonConverter<IClientState>
    {
        public override IClientState? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            IClientState? returnObj = type switch
            {
                "/ibc.lightclients.localhost.v1.ClientState" => JsonSerializer.Deserialize<LightClients.Localhost.ClientState>(ref reader, options),
                "/ibc.lightclients.solomachine.v1.ClientState" => JsonSerializer.Deserialize<LightClients.SoloMachine.ClientStateV1>(ref reader, options),
                "/ibc.lightclients.solomachine.v2.ClientState" => JsonSerializer.Deserialize<LightClients.SoloMachine.ClientState>(ref reader, options),
                "/ibc.lightclients.tendermint.v1.ClientState" => JsonSerializer.Deserialize<LightClients.Tendermint.ClientState>(ref reader, options),
                _ => throw new JsonException($"type {type} not supported"),
            };
            return returnObj;
        }

        public override void Write(Utf8JsonWriter writer, IClientState value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
