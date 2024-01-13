namespace CosmosNetwork.Modules.Transfer.Serialization
{
    internal class TransferParams
    {
        public bool ReceiveEnabled { get; set; }

        public bool SendEnabled { get; set; }

        public Transfer.TransferParams ToModel()
        {
            return new Transfer.TransferParams(this.ReceiveEnabled, this.SendEnabled);
        }
    }
}
