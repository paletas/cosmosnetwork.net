namespace CosmosNetwork.Modules.Transfer
{
    public record TransferParams(bool ReceiveEnabled, bool SendEnabled)
    {
        internal Serialization.TransferParams ToSerialization()
        {
            return new Serialization.TransferParams
            {
                ReceiveEnabled = this.ReceiveEnabled,
                SendEnabled = this.SendEnabled,
            };
        }
    }
}
