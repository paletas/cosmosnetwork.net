using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    [ProtoContract]
    internal record MessageSetWithdrawAddress(
        [property: ProtoMember(1, Name = "delegator_address")] string DelegatorAddress,
        [property: ProtoMember(2, Name = "withdraw_address")] string WithdrawAddress) : SerializerMessage(Distribution.MessageSetWithdrawAddress.COSMOS_DESCRIPTOR)
    {
        protected internal override Message ToModel()
        {
            return new Distribution.MessageSetWithdrawAddress(DelegatorAddress, WithdrawAddress);
        }
    }
}
