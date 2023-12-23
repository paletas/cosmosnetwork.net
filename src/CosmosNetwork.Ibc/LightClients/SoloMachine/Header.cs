namespace CosmosNetwork.Ibc.LightClients.SoloMachine
{
    public record Header(
        ulong Sequence,
        ulong Timestamp,
        byte[] Signature,
        IPublicKey NewPublicKey,
        string NewDiversifier) : IHeader
    {
        public Serialization.LightClients.IHeader ToSerialization()
        {
            return new Serialization.LightClients.SoloMachine.Header(
                this.Sequence,
                this.Timestamp,
                this.Signature,
                this.NewPublicKey.ToProto(),
                this.NewDiversifier);
        }
    }
}
