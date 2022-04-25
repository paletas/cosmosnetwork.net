namespace Terra.NET.Messages.Distribution
{
    public record MessageWithdrawDelegatorReward(TerraAddress Delegator, TerraAddress Validator)
        : Message(MessageTypeEnum.DistributionWithdrawRewards)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Distribution.MessageWithdrawDelegatorReward(this.Delegator.Address, this.Validator.Address);
        }
    }
}