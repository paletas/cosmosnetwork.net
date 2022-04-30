using Cosmos.SDK.Protos.Staking;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Staking
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageEditValidator(
        string ValidatorAddress, [property: JsonPropertyName("min_self_delegation")] ulong? MinimumSelfDelegation,
        ValidatorDescription Description, decimal? ComissionRate
    ) : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "staking/MsgEditValidator";
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgEditValidator";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Staking.MessageEditValidator(
                this.ValidatorAddress, this.MinimumSelfDelegation,
                new NET.Messages.Staking.ValidatorDescription(
                    this.Description.Moniker,
                    this.Description.Identity,
                    this.Description.Details,
                    this.Description.Website,
                    this.Description.SecurityContact),
                this.ComissionRate
            );
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgEditValidator
            {
                ValidatorAddress = this.ValidatorAddress,
                MinSelfDelegation = this.MinimumSelfDelegation?.ToString(),
                Description = new Description
                {
                    Moniker = this.Description.Moniker,
                    Identity = this.Description.Identity,
                    Details = this.Description.Details,
                    Website = this.Description.Website,
                    SecurityContact = this.Description.SecurityContact
                },
                CommissionRate = this.ComissionRate?.ToString(),
            };
        }
    }
}
