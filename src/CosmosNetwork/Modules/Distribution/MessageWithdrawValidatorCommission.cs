namespace CosmosNetwork.Modules.Distribution
{
    public record MessageWithdrawValidatorCommission(string MessageType, CosmosAddress Validator) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1.MsgWithdrawValidatorCommission";
        public const string COSMOS_BETA_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgWithdrawValidatorCommission";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageWithdrawValidatorCommission(this.Validator.Address);
        }
    }
}