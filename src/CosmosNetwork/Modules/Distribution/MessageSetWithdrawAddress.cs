namespace CosmosNetwork.Modules.Distribution
{
    public record MessageSetWithdrawAddress(string MessageType, CosmosAddress Delegator, CosmosAddress Withdraw) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1.MsgSetWithdrawAddress";
        public const string COSMOS_BETA_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgSetWithdrawAddress";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageSetWithdrawAddress(this.Delegator.Address, this.Withdraw.Address);
        }
    }
}