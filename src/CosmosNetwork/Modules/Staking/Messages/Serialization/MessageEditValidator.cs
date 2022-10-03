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
                ValidatorAddress, MinimumSelfDelegation,
                new Staking.ValidatorDescription(
                    Description.Moniker,
                    Description.Identity,
                    Description.Details,
                    Description.Website,
                    Description.SecurityContact),
                ComissionRate
            );
        }
    }
}
