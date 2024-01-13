using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.FeeGrant.Serialization.Allowances
{
    public record BasicAllowance(DenomAmount[] SpendLimit, DateTime? Expiration) : IAllowance
    {
        internal const string AllowanceType = "cosmos.feegrant.v1beta1.BasicAllowance";

        public string TypeUrl => AllowanceType;

        public FeeGrant.Allowances.IAllowance ToModel()
        {
            return new FeeGrant.Allowances.BasicAllowance(
                this.SpendLimit.Select(coin => coin.ToModel()).ToArray(),
                this.Expiration);
        }
    }
}
