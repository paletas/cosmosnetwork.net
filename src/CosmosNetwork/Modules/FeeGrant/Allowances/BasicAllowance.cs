using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.FeeGrant.Allowances
{
    public record BasicAllowance(Coin[] SpendLimit, DateTime? Expiration) : IAllowance
    {
        public const string AllowanceType = "/cosmos.feegrant.v1beta1.BasicAllowance";

        public Serialization.Allowances.IAllowance ToSerialization()
        {
            return new Serialization.Allowances.BasicAllowance(
                SpendLimit.Select(coin => coin.ToSerialization()).ToArray(),
                Expiration);
        }
    }
}