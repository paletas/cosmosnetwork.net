namespace Terra.NET.Messages.Oracle
{
    public record MessageDelegateFeedConsent(TerraAddress Operator, TerraAddress Delegate)
        : Message(MessageTypeEnum.OracleDelegateFeedConsent)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Oracle.MessageDelegateFeedConsent(this.Operator.Address, this.Delegate.Address);
        }
    }
}