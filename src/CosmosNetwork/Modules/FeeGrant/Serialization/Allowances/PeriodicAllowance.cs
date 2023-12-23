using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.FeeGrant.Serialization.Allowances
{
    internal record PeriodicAllowance(
        BasicAllowance Basic,
        TimeSpan Period,
        DenomAmount[] PeriodSpendLimit,
        DenomAmount[] PeriodCanSpend,
        DateTime PeriodReset) : IAllowance
    {
        internal const string AllowanceType = "cosmos.feegrant.v1beta1.PeriodicAllowance";

        public string TypeUrl => AllowanceType;

        public FeeGrant.Allowances.IAllowance ToModel()
        {
            return new FeeGrant.Allowances.PeriodicAllowance(
                (FeeGrant.Allowances.BasicAllowance)this.Basic.ToModel(),
                this.Period,
                this.PeriodSpendLimit.Select(coin => coin.ToModel()).ToArray(),
                this.PeriodCanSpend.Select(coin => coin.ToModel()).ToArray(),
                this.PeriodReset);
        }
    }
}
