using CosmosNetwork.Serialization;

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
                (Serialization.Allowances.BasicAllowance)Allowance.ToSerialization(),
                Period,
                SpendLimit.Select(coin => new DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                CanSpend.Select(coin => new DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                PeriodReset);
        }
    }
}