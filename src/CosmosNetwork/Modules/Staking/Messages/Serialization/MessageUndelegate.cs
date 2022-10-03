using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Staking.Messages.Serialization
{
    [ProtoContract]
    internal record MessageUndelegate(
        [property: ProtoMember(1, Name = "delegator_address")] string DelegatorAddress,
        [property: ProtoMember(2, Name = "validator_address")] string ValidatorAddress,
        [property: ProtoMember(3, Name = "amount")] DenomAmount Amount) : SerializerMessage(Staking.Messages.MessageUndelegate.COSMOS_DESCRIPTOR)
    {
        protected internal override Message ToModel()
        {
            return new Staking.Messages.MessageUndelegate(DelegatorAddress, ValidatorAddress, Amount.ToModel());
        }
    }
}
