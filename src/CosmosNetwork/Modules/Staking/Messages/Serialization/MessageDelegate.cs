using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Staking.Messages.Serialization
{
    [ProtoContract]
    internal record MessageDelegate(
        [property: ProtoMember(1, Name = "delegator_address")] string DelegatorAddress,
        [property: ProtoMember(2, Name = "validator_address")] string ValidatorAddress,
        [property: ProtoMember(3, Name = "amount")] DenomAmount Amount) : SerializerMessage(Staking.Messages.MessageDelegate.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Staking.Messages.MessageDelegate(this.DelegatorAddress, this.ValidatorAddress, this.Amount.ToModel());
        }
    }
}
