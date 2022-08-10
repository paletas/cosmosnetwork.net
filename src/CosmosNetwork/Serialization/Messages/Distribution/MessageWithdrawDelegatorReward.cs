namespace CosmosNetwork.Serialization.Messages.Distribution
{
    internal record MessageWithdrawDelegatorReward(
        string DelegatorAddress,
        string ValidatorAddress) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "distribution/MsgWithdrawDelegationReward";

        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Distribution.MessageWithdrawDelegatorReward(DelegatorAddress, ValidatorAddress);
        }
    }
}
