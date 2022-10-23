using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Staking.Messages.Serialization
{
    [ProtoContract]
    internal record MessageEditValidator(
        [property: ProtoMember(2, Name = "validator_address")] string ValidatorAddress,
        [property: ProtoMember(4, Name = "min_self_delegation"), JsonPropertyName("min_self_delegation")] ulong? MinimumSelfDelegation,
        [property: ProtoMember(1, Name = "description")] Staking.Serialization.ValidatorDescription Description,
        [property: ProtoMember(3, Name = "commission_rate")] decimal? ComissionRate) : SerializerMessage(Staking.Messages.MessageEditValidator.COSMOS_DESCRIPTOR)
    {
        protected internal override Message ToModel()
        {
            return new Staking.Messages.MessageEditValidator(
                this.ValidatorAddress, this.MinimumSelfDelegation,
                new Staking.ValidatorDescription(
                    this.Description.Moniker,
                    this.Description.Identity,
                    this.Description.Details,
                    this.Description.Website,
                    this.Description.SecurityContact),
                this.ComissionRate
            );
        }
    }
}
