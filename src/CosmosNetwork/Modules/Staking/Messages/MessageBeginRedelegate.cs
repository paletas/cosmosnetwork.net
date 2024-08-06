namespace CosmosNetwork.Modules.Staking.Messages
{
    public record MessageBeginRedelegate(
        string MessageType,
        CosmosAddress Delegator,
        CosmosAddress SourceValidator,
        CosmosAddress DestinationValidator,
        Coin Amount) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgBeginRedelegate";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageBeginRedelegate(
                this.Delegator.Address,
                this.SourceValidator.Address,
                this.DestinationValidator.Address,
                this.Amount.ToSerialization());
        }
    }
}