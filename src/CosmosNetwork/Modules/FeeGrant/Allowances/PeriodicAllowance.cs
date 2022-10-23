namespace CosmosNetwork.Modules.FeeGrant.Allowances
{
    public record PeriodicAllowance(
        BasicAllowance Allowance,
        TimeSpan Period,
        Coin[] SpendLimit,
        Coin[] CanSpend,
        DateTime PeriodReset) : IAllowance
    {
        public const string AllowanceType = "/cosmos.feegrant.v1beta1.PeriodicAllowance";

        public Serialization.Allowances.IAllowance ToSerialization()
        {
            return new Serialization.Allowances.PeriodicAllowance(
                (Serialization.Allowances.BasicAllowance)this.Allowance.ToSerialization(),
                this.Period,
                this.SpendLimit.Select(coin => coin.ToSerialization()).ToArray(),
                this.CanSpend.Select(coin => coin.ToSerialization()).ToArray(),
                this.PeriodReset);
        }
    }
}