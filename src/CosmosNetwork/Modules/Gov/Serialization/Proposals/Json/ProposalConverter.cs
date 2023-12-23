using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization.Proposals.Json
{
    internal class ProposalConverter : JsonConverter<IProposal>
    {
        private readonly ProposalsRegistry _registry;

        public ProposalConverter(ProposalsRegistry registry)
        {
            this._registry = registry;
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
            if (type is null)
            {
                throw new JsonException();
            }

            Type proposalType = this._registry.GetProposalByTypeName(type);
            return (IProposal?)JsonSerializer.Deserialize(ref reader, proposalType, options);
        }

        public override void Write(Utf8JsonWriter writer, IProposal value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
