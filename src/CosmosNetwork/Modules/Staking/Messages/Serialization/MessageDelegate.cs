using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Staking.Messages.Serialization
{
    internal record MessageDelegate(string DelegatorAddress, string ValidatorAddress, DenomAmount Amount) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "staking/MsgDelegate";

        protected internal override Message ToModel()
        {
            return new Staking.Messages.MessageDelegate(DelegatorAddress, ValidatorAddress, Amount.ToModel());
        }
    }
}
