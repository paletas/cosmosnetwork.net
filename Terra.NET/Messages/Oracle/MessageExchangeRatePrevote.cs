namespace Terra.NET.Messages.Oracle
{
    public record MessageExchangeRatePrevote(string Hash, TerraAddress Feeder, TerraAddress Validator)
        : Message(MessageTypeEnum.OracleExchangeRatePrevote)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Oracle.MessageExchangeRatePrevote(this.Hash, this.Feeder.Address, this.Validator.Address);
        }
    }
}