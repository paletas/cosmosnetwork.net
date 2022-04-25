namespace Terra.NET.Messages.Staking
{
    public record MessageUndelegate(TerraAddress Delegator, TerraAddress Validator, Coin Amount) : Message(MessageTypeEnum.StakingUndelegate)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Staking.MessageUndelegate(this.Delegator.Address, this.Validator.Address, new API.Serialization.Json.DenomAmount(this.Amount.Denom, this.Amount.Amount));
        }
    }
}