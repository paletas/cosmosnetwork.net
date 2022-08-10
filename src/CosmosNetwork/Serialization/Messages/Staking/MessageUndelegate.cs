namespace CosmosNetwork.Serialization.Messages.Staking
{
    internal record MessageUndelegate(string DelegatorAddress, string ValidatorAddress, DenomAmount Amount) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "staking/MsgUndelegate";

        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Staking.MessageUndelegate(DelegatorAddress, ValidatorAddress, Amount.ToModel());
        }
    }
}
