using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Staking.Messages.Serialization
{
    [ProtoContract]
    internal record MessageBeginRedelegate(
        [property: ProtoMember(1, Name = "delegator_address")] string DelegatorAddress,
        [property: ProtoMember(2, Name = "validator_src_address"), JsonPropertyName("validator_src_address")] string SourceValidatorAddress,
        [property: ProtoMember(3, Name = "validator_dst_address"), JsonPropertyName("validator_dst_address")] string DestinationValidatorAddress,
        [property: ProtoMember(4, Name = "amount")] DenomAmount Amount) : SerializerMessage(Staking.Messages.MessageBeginRedelegate.COSMOS_DESCRIPTOR)
    {
        protected internal override Message ToModel()
        {
            return new Staking.Messages.MessageBeginRedelegate(
                this.DelegatorAddress,
                this.SourceValidatorAddress,
                this.DestinationValidatorAddress,
                this.Amount.ToModel());
        }
    }
}
