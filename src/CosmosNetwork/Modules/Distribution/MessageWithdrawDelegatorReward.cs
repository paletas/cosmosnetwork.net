namespace CosmosNetwork.Modules.Distribution
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageWithdrawDelegatorReward(
        CosmosAddress Delegator,
        CosmosAddress Validator) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgWithdrawDelegatorReward";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageWithdrawDelegatorReward(this.Delegator.Address, this.Validator.Address);
        }
    }
}