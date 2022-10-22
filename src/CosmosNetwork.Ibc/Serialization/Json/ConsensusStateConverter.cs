using CosmosNetwork.Ibc.Serialization.LightClients;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Json
{
    internal class ConsensusStateConverter : JsonConverter<IConsensusState>
    {
        public override IConsensusState? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            IConsensusState? returnObj = type switch
            {
                "/ibc.lightclients.solomachine.v1.ConsensusState" => JsonSerializer.Deserialize<LightClients.SoloMachine.ConsensusStateV1>(ref reader, options),
                "/ibc.lightclients.solomachine.v2.ConsensusState" => JsonSerializer.Deserialize<LightClients.SoloMachine.ConsensusState>(ref reader, options),
                "/ibc.lightclients.tendermint.v1.ConsensusState" => JsonSerializer.Deserialize<LightClients.Tendermint.ConsensusState>(ref reader, options),
                _ => throw new JsonException($"type {type} not supported"),
            };
            return returnObj;
        }

        public override void Write(Utf8JsonWriter writer, IConsensusState value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
