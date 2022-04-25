namespace Terra.NET.Messages.Oracle
{
    public record MessageExchangeRateVote(string Salt, TerraAddress Feeder, TerraAddress Validator, DenomSwapRate[] ExchangeRates)
        : Message(MessageTypeEnum.OracleExchangeRateVote)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Oracle.MessageExchangeRateVote(this.Salt, this.Feeder.Address, this.Validator.Address, API.Serialization.Json.Messages.Oracle.MessageExchangeRateVote.EncodeExchangeRates(this.ExchangeRates));
        }
    }
}