namespace Terra.NET.Messages.Staking
{
    public record MessageDelegate(TerraAddress Delegator, TerraAddress Validator, Coin Amount)
        : Message(MessageTypeEnum.StakingDelegate)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Staking.MessageDelegate(
                this.Delegator.Address,
                this.Validator.Address,
                new API.Serialization.Json.DenomAmount(this.Amount.Denom, this.Amount.Amount));
        }
    }
}