using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    [ProtoContract]
    internal record MessageWithdrawDelegatorReward(
        [property: ProtoMember(1, Name = "delegator_address")] string DelegatorAddress,
        [property: ProtoMember(2, Name = "validator_address")] string ValidatorAddress) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "distribution/MsgWithdrawDelegationReward";

        protected internal override Message ToModel()
        {
            return new Distribution.MessageWithdrawDelegatorReward(DelegatorAddress, ValidatorAddress);
        }
    }
}
