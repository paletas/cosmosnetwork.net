namespace CosmosNetwork.Messages.Distribution
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageSetWithdrawAddress(CosmosAddress Delegator, CosmosAddress Withdraw) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgSetWithdrawAddress";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Distribution.MessageSetWithdrawAddress(Delegator.Address, Withdraw.Address);
        }
    }
}