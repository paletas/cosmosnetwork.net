namespace CosmosNetwork.Modules.Auth.Serialization.Accounts
{
    internal abstract class Account
    {
        public AccountTypes Type { get; set; }

        public abstract Auth.BaseAccount ToModel();
    }
}
