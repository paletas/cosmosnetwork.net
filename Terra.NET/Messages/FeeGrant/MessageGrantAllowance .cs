namespace Terra.NET.Messages.FeeGrant
{
    public record MessageGrantAllowance(TerraAddress Granter, TerraAddress Grantee, Allowance Allowance)
        : Message(MessageTypeEnum.FeeGrantAllowance)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.FeeGrant.MessageGrantAllowance(
                this.Granter.Address,
                this.Grantee.Address,
                this.Allowance.ToJson());
        }
    }

    public enum AllowanceTypeEnum
    {
        Basic,
        Periodic,
        Message
    }

    public abstract record Allowance(AllowanceTypeEnum AllowanceType)
    {
        internal abstract API.Serialization.Json.Messages.FeeGrant.IAllowance ToJson();
    }

    public record BasicAllowance(Coin[] SpendLimit, DateTime? Expiration) : Allowance(AllowanceTypeEnum.Basic)
    {
        internal override API.Serialization.Json.Messages.FeeGrant.IAllowance ToJson()
        {
            return ToBasicAllowanceJson();
        }

        internal API.Serialization.Json.Messages.FeeGrant.BasicAllowance ToBasicAllowanceJson()
        {
            return new API.Serialization.Json.Messages.FeeGrant.BasicAllowance(
                this.SpendLimit.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                this.Expiration);
        }
    }

    public record PeriodicAllowance(
        BasicAllowance Allowance,
        TimeSpan Period,
        Coin[] SpendLimit,
        Coin[] CanSpend,
        DateTime PeriodReset) : Allowance(AllowanceTypeEnum.Periodic)
    {
        internal override API.Serialization.Json.Messages.FeeGrant.IAllowance ToJson()
        {
            return new API.Serialization.Json.Messages.FeeGrant.PeriodicAllowance(
                this.Allowance.ToBasicAllowanceJson(),
                this.Period,
                this.SpendLimit.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                this.CanSpend.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                this.PeriodReset);
        }
    }

    public record AllowedMessageAllowance(Allowance Allowance, string[] AllowedMessages)
        : Allowance(AllowanceTypeEnum.Message)
    {
        internal override API.Serialization.Json.Messages.FeeGrant.IAllowance ToJson()
        {
            return new API.Serialization.Json.Messages.FeeGrant.AllowedMsgAllowance(
                this.Allowance.ToJson(),
                this.AllowedMessages);
        }
    }
}