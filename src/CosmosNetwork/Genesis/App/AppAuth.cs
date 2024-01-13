using CosmosNetwork.Modules.Auth;

namespace CosmosNetwork.Genesis.App
{
    public record AppAuth(
        BaseAccount[] Accounts,
        AuthParams Params)
    {
        internal Serialization.App.AppAuth ToSerialization()
        {
            return new Serialization.App.AppAuth
            {
                Accounts = this.Accounts.Select(acc => acc.ToSerialization()).ToArray(),
                Params = this.Params.ToSerialization()
            };
        }
    }
}
