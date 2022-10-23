using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.FeeGrant.Serialization.Allowances.Json
{
    internal class AllowancesConverter : JsonConverter<IAllowance[]>
    {
        private readonly AuthorizationRegistry _registry;

        public AllowancesConverter(AuthorizationRegistry registry)
        {
            this._registry = registry;
        }

        public override IAllowance[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            serializerOptions.Converters.Add(new AllowanceConverter(this._registry));

            List<IAllowance> allowances = new();
            do
            {
                IAllowance? allowance = JsonSerializer.Deserialize<IAllowance>(ref reader, serializerOptions);
                if (allowance == null)
                {
                    throw new JsonException();
                }

                allowances.Add(allowance);
            }
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray);

            return allowances.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, IAllowance[] value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    internal class AllowanceConverter : JsonConverter<IAllowance>
    {
        private readonly AuthorizationRegistry _registry;

        public AllowanceConverter(AuthorizationRegistry registry)
        {
            this._registry = registry;
        }

        public override IAllowance? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            return (IAllowance?)JsonSerializer.Deserialize(ref reader, proposalType, options);
        }

        public override void Write(Utf8JsonWriter writer, IAllowance value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
