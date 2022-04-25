namespace Terra.NET.Messages.Slashing
{
    public record MessageUnjail(TerraAddress Validator)
        : Message(MessageTypeEnum.OracleExchangeRateVote)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Slashing.MessageUnjail(this.Validator.Address);
        }
    }
}