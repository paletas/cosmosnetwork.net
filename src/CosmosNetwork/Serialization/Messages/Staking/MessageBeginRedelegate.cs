using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Staking
{
    internal record MessageBeginRedelegate(
        string DelegatorAddress,
        [property: JsonPropertyName("validator_src_address")] string SourceValidatorAddress,
        [property: JsonPropertyName("validator_dst_address")] string DestinationValidatorAddress,
        DenomAmount Amount) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Staking.MessageBeginRedelegate(
                DelegatorAddress,
                SourceValidatorAddress,
                DestinationValidatorAddress,
                Amount.ToModel());
        }
    }
}
