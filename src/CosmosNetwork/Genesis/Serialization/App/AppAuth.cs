using CosmosNetwork.Modules.Auth.Serialization;
using CosmosNetwork.Modules.Auth.Serialization.Accounts;

namespace CosmosNetwork.Genesis.Serialization.App
{
    internal class AppAuth
    {
        public Account[] Accounts { get; set; }

        public AuthParams Params { get; set; }

        public Genesis.App.AppAuth ToModel()
        {
            return new Genesis.App.AppAuth(
                this.Accounts.Select(acc => acc.ToModel()).ToArray(),
                this.Params.ToModel());
        }
    }
}
