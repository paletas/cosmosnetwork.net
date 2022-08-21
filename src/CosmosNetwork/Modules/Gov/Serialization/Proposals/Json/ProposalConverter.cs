using CosmosNetwork.Modules.Gov.Proposals;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization.Proposals.Json
{
    internal class ProposalConverter : JsonConverter<IProposal>
    {
        private readonly ProposalRegistry _registry;

        public ProposalConverter(ProposalRegistry registry)
        {
            _registry = registry;
        }

        public override IProposal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            Type proposalType = _registry.GetProposalByTypeName(type);
            return (IProposal?)JsonSerializer.Deserialize(ref reader, proposalType, options);
        }

        public override void Write(Utf8JsonWriter writer, IProposal value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
