using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Staking.Messages.Serialization
{
    internal record MessageCreateValidator(
        string DelegatorAddress,
        string ValidatorAddress,
        [property: JsonPropertyName("min_self_delegation")] ulong MinimumSelfDelegation,
        Staking.Serialization.ValidatorDescription Description,
        Staking.Serialization.ValidatorCommissionRates Commission,
        DenomAmount Value,
        [property: JsonPropertyName("pubkey"), JsonConverter(typeof(PublicKeyConverter))] CosmosNetwork.Serialization.Json.PublicKey PublicKey
    ) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new Staking.Messages.MessageCreateValidator(
                DelegatorAddress, ValidatorAddress, MinimumSelfDelegation,
                new Staking.ValidatorDescription(Description.Moniker, Description.Identity, Description.Details, Description.Website, Description.SecurityContact),
                new Staking.ValidatorCommissionRates(Commission.Rate, Commission.MaxRate, Commission.MaxRateChange),
                Value.ToModel(),
                PublicKey.ToModel()
            );
        }
    }

}
