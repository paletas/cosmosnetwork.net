namespace CosmosNetwork.Messages.Staking
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageDelegate(CosmosAddress Delegator, CosmosAddress Validator, Coin Amount) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgDelegate";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Staking.MessageDelegate(
                Delegator.Address,
                Validator.Address,
                new Serialization.DenomAmount(Amount.Denom, Amount.Amount));
        }
    }
}