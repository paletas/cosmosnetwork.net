using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Staking.Serialization
{
    internal record MessageUndelegate(string DelegatorAddress, string ValidatorAddress, DenomAmount Amount) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "staking/MsgUndelegate";

        protected internal override Message ToModel()
        {
            return new Staking.MessageUndelegate(DelegatorAddress, ValidatorAddress, Amount.ToModel());
        }
    }
}
