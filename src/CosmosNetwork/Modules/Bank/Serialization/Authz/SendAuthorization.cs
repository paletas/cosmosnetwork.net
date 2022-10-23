using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Bank.Serialization.Authz
{
    internal class SendAuthorization : IAuthorization
    {
        public string TypeUrl { get; set; } = null!;

        public DenomAmount[] SpendLimit { get; set; } = Array.Empty<DenomAmount>();

        public Modules.Authz.Authorizations.IAuthorization ToModel()
        {
            return new Bank.Authz.SendAuthorization
            {
                SpendLimit = this.SpendLimit.Select(amt => amt.ToModel()).ToArray()
            };
        }
    }
}
