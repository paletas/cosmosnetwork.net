namespace CosmosNetwork.Modules.Staking.Messages
{
    public record MessageUndelegate(string MessageType, CosmosAddress Delegator, CosmosAddress Validator, Coin Amount) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgUndelegate";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageUndelegate(
                this.Delegator.Address,
                this.Validator.Address,
                this.Amount.ToSerialization());
        }
    }
}