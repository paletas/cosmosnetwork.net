using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    [ProtoContract]
    public record MessageSetWithdrawAddress(
        [property: ProtoMember(1, Name = "delegator_address")] string DelegatorAddress,
        [property: ProtoMember(2, Name = "withdraw_address")] string WithdrawAddress) : SerializerMessage(Distribution.MessageSetWithdrawAddress.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Distribution.MessageSetWithdrawAddress(this.DelegatorAddress, this.WithdrawAddress);
        }
    }
}
