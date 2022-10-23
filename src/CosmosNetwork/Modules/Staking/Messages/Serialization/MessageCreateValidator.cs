using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Json.Converters;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Staking.Messages.Serialization
{
    [ProtoContract]
    internal record MessageCreateValidator(
        [property: ProtoMember(4, Name = "delegator_address")] string DelegatorAddress,
        [property: ProtoMember(5, Name = "validator_address")] string ValidatorAddress,
        [property: ProtoMember(3, Name = "min_self_delegation"), JsonPropertyName("min_self_delegation")] ulong MinimumSelfDelegation,
        [property: ProtoMember(1, Name = "description")] Staking.Serialization.ValidatorDescription Description,
        [property: ProtoMember(2, Name = "commission")] Staking.Serialization.ValidatorCommissionRates Commission,
        [property: ProtoMember(7, Name = "value")] DenomAmount Value,
        [property: JsonPropertyName("pubkey"), JsonConverter(typeof(PublicKeyConverter))] CosmosNetwork.Serialization.Json.PublicKey PublicKeyJson,
        [property: ProtoMember(6, Name = "pubkey")] CosmosNetwork.Serialization.Proto.SimplePublicKey PublicKeyProto
    ) : SerializerMessage(Messages.MessageCreateValidator.COSMOS_DESCRIPTOR)
    {
        protected internal override Message ToModel()
        {
            return new Staking.Messages.MessageCreateValidator(
                this.DelegatorAddress, this.ValidatorAddress, this.MinimumSelfDelegation,
                new Staking.ValidatorDescription(this.Description.Moniker, this.Description.Identity, this.Description.Details, this.Description.Website, this.Description.SecurityContact),
                new Staking.ValidatorCommissionRates(this.Commission.Rate, this.Commission.MaxRate, this.Commission.MaxRateChange),
                this.Value.ToModel(),
                this.PublicKeyJson.ToModel());
        }
    }

}
