namespace CosmosNetwork.Modules.Bank
{
    public record BankParams(bool SendEnableDefault, string[] SendEnabled)
    {
        internal Serialization.BankParams ToSerialization()
        {
            return new Serialization.BankParams
            {
                DefaultSendEnabled = this.SendEnableDefault,
                SendEnabled = this.SendEnabled
            };
        }
    }
}
