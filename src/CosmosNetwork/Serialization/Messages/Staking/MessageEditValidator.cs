using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Staking
{
    internal record MessageEditValidator(
        string ValidatorAddress,
        [property: JsonPropertyName("min_self_delegation")] ulong? MinimumSelfDelegation,
        ValidatorDescription Description,
        decimal? ComissionRate) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "staking/MsgEditValidator";
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgEditValidator";

        internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Staking.MessageEditValidator(
                ValidatorAddress, MinimumSelfDelegation,
                new CosmosNetwork.Messages.Staking.ValidatorDescription(
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
