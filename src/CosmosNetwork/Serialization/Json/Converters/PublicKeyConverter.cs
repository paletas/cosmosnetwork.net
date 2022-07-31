using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
    internal class PublicKeyConverter : JsonConverter<PublicKey>
    {
        public override PublicKey? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            PublicKey? returnObj = type switch
            {
                "/cosmos.crypto.secp256k1.PubKey" => JsonSerializer.Deserialize<Secp256k1>(ref reader, options),
                "/cosmos.crypto.ed25519.PubKey" => JsonSerializer.Deserialize<Ed25519>(ref reader, options),
                "/cosmos.crypto.multisig.LegacyAminoPubKey" => JsonSerializer.Deserialize<MultisigKey>(ref reader, options),
                _ => throw new JsonException($"type {type} not supported"),
            };
            return returnObj;
        }

        public override void Write(Utf8JsonWriter writer, PublicKey value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    internal class PublicKeysConverter : JsonConverter<PublicKey[]>
    {
        public override PublicKey[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var serializerOptions = new JsonSerializerOptions(options);
            serializerOptions.Converters.Add(new PublicKeyConverter());

            var keys = new List<PublicKey>();
            do
            {
                var key = JsonSerializer.Deserialize<PublicKey>(ref reader, serializerOptions);
                if (key == null) throw new JsonException();

                keys.Add(key);
            }
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray);

            return keys.ToArray();
        }


        public override void Write(Utf8JsonWriter writer, PublicKey[] value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
