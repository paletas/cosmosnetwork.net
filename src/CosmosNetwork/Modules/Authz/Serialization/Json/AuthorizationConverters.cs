using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Authz.Serialization.Json
{
    internal class AuthorizationsConverter : JsonConverter<Serialization.Authorizations.IAuthorization[]>
    {
        private readonly AuthorizationRegistry _registry;

        public AuthorizationsConverter(AuthorizationRegistry registry)
        {
            _registry = registry;
        }

        public override Authorizations.IAuthorization[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            JsonSerializerOptions serializerOptions = new(options);
            serializerOptions.Converters.Add(new AuthorizationConverter(_registry));

            List<Authorizations.IAuthorization> authorizations = new();
            do
            {
                Authorizations.IAuthorization? authorization = JsonSerializer.Deserialize<Authorizations.IAuthorization>(ref reader, serializerOptions);
                if (authorization == null)
                {
                    throw new JsonException();
                }

                authorizations.Add(authorization);
            }
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray);

            return authorizations.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, Authorizations.IAuthorization[] value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    internal class AuthorizationConverter : JsonConverter<Authorizations.IAuthorization>
    {
        private readonly AuthorizationRegistry _registry;

        public AuthorizationConverter(AuthorizationRegistry registry)
        {
            _registry = registry;
        }

        public override Authorizations.IAuthorization? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

            Type proposalType = _registry.GetProposalByTypeName(type);
            return (Authorizations.IAuthorization?)JsonSerializer.Deserialize(ref reader, proposalType, options);
        }

        public override void Write(Utf8JsonWriter writer, Authorizations.IAuthorization value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
