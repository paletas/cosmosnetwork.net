namespace CosmosNetwork.Modules.Auth.Serialization.Accounts
{
    internal class ModuleAccount : Account
    {
        public BaseAccount BaseAccount { get; set; }

        public string Name { get; set; }

        public string[] Permissions { get; set; }

        public override Auth.BaseAccount ToModel()
        {
            return new Auth.ModuleAccount(
                this.BaseAccount.Address,
                this.BaseAccount.Sequence,
                this.BaseAccount.AccountNumber,
                this.Name);
        }
    }
}
