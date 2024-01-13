namespace CosmosNetwork.Modules.Auth.Serialization.Accounts
{
    internal class BaseAccount : Account
    {
        public string Address { get; set; }

        public int Sequence { get; set; }

        public string AccountNumber { get; set; }

        public override Auth.BaseAccount ToModel()
        {
            return new Auth.BaseAccount(
                this.Address,
                this.Sequence,
                this.AccountNumber);
        }
    }
}
