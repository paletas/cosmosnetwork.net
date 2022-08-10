namespace CosmosNetwork.Messages.Distribution
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageWithdrawDelegatorReward(
        CosmosAddress Delegator,
        CosmosAddress Validator) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgWithdrawDelegatorReward";

        protected internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Distribution.MessageWithdrawDelegatorReward(Delegator.Address, Validator.Address);
        }
    }
}