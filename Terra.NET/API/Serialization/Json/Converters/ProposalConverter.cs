using System.Text.Json;
using System.Text.Json.Serialization;
using Terra.NET.API.Serialization.Json.Messages.Gov;

namespace Terra.NET.API.Serialization.Json.Converters
{
    internal class ProposalConverter : JsonConverter<IProposal>
    {
        public override IProposal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var innerReader = reader; //local copy

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

            var type = innerReader.GetString();
            IProposal? returnObj = type switch
            {
                "/cosmos.gov.v1beta1.TextProposal" => JsonSerializer.Deserialize<TextProposal>(ref reader, options),
                "/cosmos.distribution.v1beta1.CommunityPoolSpendProposal" => JsonSerializer.Deserialize<CommunitySpendProposal>(ref reader, options),
                _ => throw new JsonException($"type {type} not supported"),
            };
            return returnObj;
        }

        public override void Write(Utf8JsonWriter writer, IProposal value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
