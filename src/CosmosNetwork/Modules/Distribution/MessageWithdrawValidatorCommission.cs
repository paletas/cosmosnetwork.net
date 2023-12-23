namespace CosmosNetwork.Modules.Distribution
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageWithdrawValidatorCommission(CosmosAddress Validator) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgWithdrawValidatorCommission";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageWithdrawValidatorCommission(this.Validator.Address);
        }
    }
}