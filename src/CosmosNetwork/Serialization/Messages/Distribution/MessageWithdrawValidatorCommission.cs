namespace CosmosNetwork.Serialization.Messages.Distribution
{
    internal record MessageWithdrawValidatorCommission(string ValidatorAddress) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "distribution/MsgWithdrawValidatorCommission";

        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Distribution.MessageWithdrawValidatorCommission(ValidatorAddress);
        }
    }
}
