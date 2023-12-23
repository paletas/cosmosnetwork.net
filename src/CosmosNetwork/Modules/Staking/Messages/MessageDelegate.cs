namespace CosmosNetwork.Modules.Staking.Messages
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageDelegate(CosmosAddress Delegator, CosmosAddress Validator, Coin Amount) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgDelegate";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageDelegate(
                this.Delegator.Address,
                this.Validator.Address,
                this.Amount.ToSerialization());
        }
    }
}