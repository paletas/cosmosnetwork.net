using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Staking.Serialization
{
    internal record MessageBeginRedelegate(
        string DelegatorAddress,
        [property: JsonPropertyName("validator_src_address")] string SourceValidatorAddress,
        [property: JsonPropertyName("validator_dst_address")] string DestinationValidatorAddress,
        DenomAmount Amount) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new Staking.MessageBeginRedelegate(
                DelegatorAddress,
                SourceValidatorAddress,
                DestinationValidatorAddress,
                Amount.ToModel());
        }
    }
}
