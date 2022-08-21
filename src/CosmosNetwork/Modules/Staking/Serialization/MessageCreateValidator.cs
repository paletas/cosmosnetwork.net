using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Staking.Serialization
{
    internal record MessageCreateValidator(
        string DelegatorAddress,
        string ValidatorAddress,
        [property: JsonPropertyName("min_self_delegation")] ulong MinimumSelfDelegation,
        ValidatorDescription Description,
        ValidatorComission Commission,
        DenomAmount Value,
        [property: JsonPropertyName("pubkey"), JsonConverter(typeof(PublicKeyConverter))] CosmosNetwork.Serialization.PublicKey PublicKey
    ) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new Staking.MessageCreateValidator(
                DelegatorAddress, ValidatorAddress, MinimumSelfDelegation,
                new Staking.ValidatorDescription(Description.Moniker, Description.Identity, Description.Details, Description.Website, Description.SecurityContact),
                new Staking.ValidatorComission(Commission.Rate, Commission.MaxRate, Commission.MaxRateChange),
                Value.ToModel(),
                PublicKey.ToModel()
            );
        }
    }

    internal record ValidatorDescription(string Moniker, string Identity, string Details, string Website, string SecurityContact);

    internal record ValidatorComission(decimal Rate, decimal MaxRate, decimal MaxRateChange);

}
