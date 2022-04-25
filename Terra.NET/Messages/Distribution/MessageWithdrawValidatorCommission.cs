namespace Terra.NET.Messages.Distribution
{
    public record MessageWithdrawValidatorCommission(TerraAddress Validator)
        : Message(MessageTypeEnum.DistributionWithdrawCommission)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Distribution.MessageWithdrawValidatorCommission(this.Validator.Address);
        }
    }
}