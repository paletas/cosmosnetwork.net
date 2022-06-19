using Cosmos.SDK.Protos.Staking;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;
using Terra.NET.API.Serialization.Json.Converters;

namespace Terra.NET.API.Serialization.Json.Messages.Staking
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageCreateValidator(
        string DelegatorAddress, string ValidatorAddress, [property: JsonPropertyName("min_self_delegation")] ulong MinimumSelfDelegation,
        ValidatorDescription Description, ValidatorComission Commission, DenomAmount Value,
        [property: JsonPropertyName("pubkey"), JsonConverter(typeof(PublicKeyConverter))] PublicKey PublicKey
    ) : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "staking/MsgCreateValidator";
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgCreateValidator";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Staking.MessageCreateValidator(
                this.DelegatorAddress, this.ValidatorAddress, this.MinimumSelfDelegation,
                new NET.Messages.Staking.ValidatorDescription(this.Description.Moniker, this.Description.Identity, this.Description.Details, this.Description.Website, this.Description.SecurityContact),
                new NET.Messages.Staking.ValidatorComission(this.Commission.Rate, this.Commission.MaxRate, this.Commission.MaxRateChange),
                new NativeCoin(this.Value.Denom, this.Value.Amount),
                this.PublicKey.ToModel()
            );
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var undelegate = new MsgCreateValidator
            {
                DelegatorAddress = this.DelegatorAddress,
                ValidatorAddress = this.ValidatorAddress,
                MinSelfDelegation = this.MinimumSelfDelegation.ToString(),
                Description = new Description
                {
                    Moniker = this.Description.Moniker,
                    Identity = this.Description.Identity,
                    Details = this.Description.Details,
                    Website = this.Description.Website,
                    SecurityContact = this.Description.SecurityContact
                },
                Commission = new CommissionRates
                {
                    Rate = this.Commission.Rate.ToString(),
                    MaxRate = this.Commission.MaxRate.ToString(),
                    MaxChangeRate = this.Commission.MaxRateChange.ToString()
                },
                Value = this.Value.ToProto(),
                Pubkey = this.PublicKey.PackAny()
            };

            return undelegate;
        }
    }

    internal record ValidatorDescription(string Moniker, string Identity, string Details, string Website, string SecurityContact);

    internal record ValidatorComission(decimal Rate, decimal MaxRate, decimal MaxRateChange);

}
