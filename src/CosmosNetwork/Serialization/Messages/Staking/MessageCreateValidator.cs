using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Staking
{
    internal record MessageCreateValidator(
        string DelegatorAddress,
        string ValidatorAddress,
        [property: JsonPropertyName("min_self_delegation")] ulong MinimumSelfDelegation,
        ValidatorDescription Description,
        ValidatorComission Commission,
        DenomAmount Value,
        [property: JsonPropertyName("pubkey"), JsonConverter(typeof(PublicKeyConverter))] PublicKey PublicKey
    ) : SerializerMessage
    {
        internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Staking.MessageCreateValidator(
                DelegatorAddress, ValidatorAddress, MinimumSelfDelegation,
                new CosmosNetwork.Messages.Staking.ValidatorDescription(Description.Moniker, Description.Identity, Description.Details, Description.Website, Description.SecurityContact),
                new CosmosNetwork.Messages.Staking.ValidatorComission(Commission.Rate, Commission.MaxRate, Commission.MaxRateChange),
                new NativeCoin(Value.Denom, Value.Amount),
                PublicKey.ToModel()
            );
        }
    }

    internal record ValidatorDescription(string Moniker, string Identity, string Details, string Website, string SecurityContact);

    internal record ValidatorComission(decimal Rate, decimal MaxRate, decimal MaxRateChange);

}
