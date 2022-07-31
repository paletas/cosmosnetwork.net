namespace CosmosNetwork.Serialization.Messages.Staking
{
    internal record MessageDelegate(string DelegatorAddress, string ValidatorAddress, DenomAmount Amount) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "staking/MsgDelegate";

        internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Staking.MessageDelegate(DelegatorAddress, ValidatorAddress, Amount.ToModel());
        }
    }
}
