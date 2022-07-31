using CosmosNetwork.Serialization.Messages.FeeGrant;

namespace CosmosNetwork.Messages.FeeGrant
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageGrantAllowance(CosmosAddress Granter, CosmosAddress Grantee, Allowance Allowance) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.feegrant.v1beta1.MsgGrantAllowance";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.FeeGrant.MessageGrantAllowance(
                Granter.Address,
                Grantee.Address,
                Allowance.ToJson());
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
        internal abstract IAllowance ToJson();
    }

    public record BasicAllowance(Coin[] SpendLimit, DateTime? Expiration) : Allowance(AllowanceTypeEnum.Basic)
    {
        internal override IAllowance ToJson()
        {
            return ToBasicAllowanceJson();
        }

        internal Serialization.Messages.FeeGrant.BasicAllowance ToBasicAllowanceJson()
        {
            return new Serialization.Messages.FeeGrant.BasicAllowance(
                SpendLimit.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                Expiration);
        }
    }

    public record PeriodicAllowance(
        BasicAllowance Allowance,
        TimeSpan Period,
        Coin[] SpendLimit,
        Coin[] CanSpend,
        DateTime PeriodReset) : Allowance(AllowanceTypeEnum.Periodic)
    {
        internal override IAllowance ToJson()
        {
            return new Serialization.Messages.FeeGrant.PeriodicAllowance(
                Allowance.ToBasicAllowanceJson(),
                Period,
                SpendLimit.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                CanSpend.Select(coin => new Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                PeriodReset);
        }
    }

    public record AllowedMessageAllowance(Allowance Allowance, string[] AllowedMessages)
        : Allowance(AllowanceTypeEnum.Message)
    {
        internal override IAllowance ToJson()
        {
            return new Serialization.Messages.FeeGrant.AllowedMsgAllowance(
                Allowance.ToJson(),
                AllowedMessages);
        }
    }
}