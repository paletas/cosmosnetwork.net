namespace CosmosNetwork.Modules.Bank.Serialization
{
    internal class BankParams
    {
        public bool DefaultSendEnabled { get; set; }

        public string[] SendEnabled { get; set; }

        public Bank.BankParams ToModel()
        {
            return new Bank.BankParams(this.DefaultSendEnabled, this.SendEnabled);
        }
    }
}
