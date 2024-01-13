using CosmosNetwork.Modules.Auth.Serialization.Accounts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Auth.Serialization.Json
{
    internal class AccountConverter : JsonConverter<Account>
    {
        public override Account? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

            Account? account = type switch
            {
                "/cosmos.auth.v1beta1.BaseAccount" => JsonSerializer.Deserialize<Serialization.Accounts.BaseAccount>(ref reader, options),
                "/cosmos.vesting.v1beta1.DelayedVestingAccount" => JsonSerializer.Deserialize<Serialization.Accounts.DelayedVestingAccount>(ref reader, options),
                "/cosmos.vesting.v1beta1.PeriodicVestingAccount" => JsonSerializer.Deserialize<Serialization.Accounts.PeriodicVestingAccount>(ref reader, options),
                "/cosmos.vesting.v1beta1.ContinuousVestingAccount" => JsonSerializer.Deserialize<Serialization.Accounts.ContinuousVestingAccount>(ref reader, options),
                "/cosmos.auth.v1beta1.ModuleAccount" => JsonSerializer.Deserialize<Serialization.Accounts.ModuleAccount>(ref reader, options),
                _ => throw new NotImplementedException($"type '{type}' not supported"),
            };
            return account;
        }

        public override void Write(Utf8JsonWriter writer, Account value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
