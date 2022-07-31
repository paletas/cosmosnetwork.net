namespace CosmosNetwork.Messages.Staking
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageUndelegate(CosmosAddress Delegator, CosmosAddress Validator, Coin Amount) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgUndelegate";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Staking.MessageUndelegate(
                Delegator.Address,
                Validator.Address,
                new Serialization.DenomAmount(Amount.Denom, Amount.Amount));
        }
    }
}