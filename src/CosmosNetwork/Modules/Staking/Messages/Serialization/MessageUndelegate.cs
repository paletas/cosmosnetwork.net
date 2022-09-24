using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Staking.Messages.Serialization
{
    internal record MessageUndelegate(string DelegatorAddress, string ValidatorAddress, DenomAmount Amount) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "staking/MsgUndelegate";

        protected internal override Message ToModel()
        {
            return new Staking.Messages.MessageUndelegate(DelegatorAddress, ValidatorAddress, Amount.ToModel());
        }
    }
}
