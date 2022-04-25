namespace Terra.NET.Messages.Staking
{
    public record MessageBeginRedelegate(TerraAddress Delegator, TerraAddress SourceValidator, TerraAddress DestinationValidator, Coin Amount)
        : Message(MessageTypeEnum.StakingBeginRedelegate)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Staking.MessageBeginRedelegate(this.Delegator.Address, this.SourceValidator.Address, this.DestinationValidator.Address, new API.Serialization.Json.DenomAmount(this.Amount.Denom, this.Amount.Amount));
        }
    }
}