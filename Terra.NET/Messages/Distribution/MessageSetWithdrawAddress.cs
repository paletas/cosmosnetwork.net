namespace Terra.NET.Messages.Distribution
{
    public record MessageSetWithdrawAddress(TerraAddress Delegator, TerraAddress Withdraw)
        : Message(MessageTypeEnum.StakingSetWithdrawAddress)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Distribution.MessageSetWithdrawAddress(this.Delegator.Address, this.Withdraw.Address);
        }
    }
}