using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    [ProtoContract]
    public record MessageWithdrawDelegatorReward(
        [property: ProtoMember(1, Name = "delegator_address")] string DelegatorAddress,
        [property: ProtoMember(2, Name = "validator_address")] string ValidatorAddress) : SerializerMessage(Distribution.MessageWithdrawDelegatorReward.COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "distribution/MsgWithdrawDelegationReward";

        public override Message ToModel()
        {
            return new Distribution.MessageWithdrawDelegatorReward(this.DelegatorAddress, this.ValidatorAddress);
        }
    }
}
