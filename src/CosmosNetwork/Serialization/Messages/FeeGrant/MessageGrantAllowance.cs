using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.FeeGrant
{
    internal record MessageGrantAllowance(
        [property: JsonPropertyName("granter")] string GranterAddress,
        [property: JsonPropertyName("grantee")] string GranteeAddress,
        IAllowance Allowance) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "feegrant/MsgGrantAllowance";

        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.FeeGrant.MessageGrantAllowance(GranterAddress, GranteeAddress, Allowance.ToModel());
        }
    }

    internal interface IAllowance
    {
        CosmosNetwork.Messages.FeeGrant.Allowance ToModel();
    }

    internal record BasicAllowance(DenomAmount[] SpendLimit, DateTime? Expiration) : IAllowance
    {
        public CosmosNetwork.Messages.FeeGrant.Allowance ToModel()
        {
            return new CosmosNetwork.Messages.FeeGrant.BasicAllowance(
                SpendLimit.Select(coin => coin.ToModel()).ToArray(),
                Expiration);
        }
    }

    internal record PeriodicAllowance(BasicAllowance Basic, TimeSpan Period, DenomAmount[] PeriodSpendLimit, DenomAmount[] PeriodCanSpend, DateTime PeriodReset) : IAllowance
    {
        public CosmosNetwork.Messages.FeeGrant.Allowance ToModel()
        {
            return new CosmosNetwork.Messages.FeeGrant.PeriodicAllowance(
                (CosmosNetwork.Messages.FeeGrant.BasicAllowance)Basic.ToModel(),
                Period,
                PeriodSpendLimit.Select(coin => coin.ToModel()).ToArray(),
                PeriodCanSpend.Select(coin => coin.ToModel()).ToArray(),
                PeriodReset);
        }
    }

    internal record AllowedMsgAllowance(IAllowance Allowance, string[] AllowedMessages) : IAllowance
    {
        public CosmosNetwork.Messages.FeeGrant.Allowance ToModel()
        {
            return new CosmosNetwork.Messages.FeeGrant.AllowedMessageAllowance(
                Allowance.ToModel(),
                AllowedMessages);
        }
    }
}
