namespace CosmosNetwork.Messages.Staking
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageBeginRedelegate(
        CosmosAddress Delegator,
        CosmosAddress SourceValidator,
        CosmosAddress DestinationValidator,
        Coin Amount) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgBeginRedelegate";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Staking.MessageBeginRedelegate(
                Delegator.Address,
                SourceValidator.Address,
                DestinationValidator.Address,
                new Serialization.DenomAmount(Amount.Denom, Amount.Amount));
        }
    }
}