namespace CosmosNetwork.Messages.Distribution
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageWithdrawValidatorCommission(CosmosAddress Validator) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgWithdrawValidatorCommission";

        protected internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Distribution.MessageWithdrawValidatorCommission(Validator.Address);
        }
    }
}