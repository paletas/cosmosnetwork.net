namespace CosmosNetwork.Modules.Distribution
{
    public record MessageWithdrawDelegatorReward(
        string MessageType,
        CosmosAddress Delegator,
        CosmosAddress Validator) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1.MsgWithdrawDelegatorReward";
        public const string COSMOS_BETA_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgWithdrawDelegatorReward";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageWithdrawDelegatorReward(this.Delegator.Address, this.Validator.Address);
        }
    }
}