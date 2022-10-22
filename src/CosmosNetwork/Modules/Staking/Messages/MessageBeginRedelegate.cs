namespace CosmosNetwork.Modules.Staking.Messages
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageBeginRedelegate(
        CosmosAddress Delegator,
        CosmosAddress SourceValidator,
        CosmosAddress DestinationValidator,
        Coin Amount) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgBeginRedelegate";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageBeginRedelegate(
                Delegator.Address,
                SourceValidator.Address,
                DestinationValidator.Address,
                Amount.ToSerialization());
        }
    }
}