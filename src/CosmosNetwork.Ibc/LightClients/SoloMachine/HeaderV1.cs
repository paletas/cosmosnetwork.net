namespace CosmosNetwork.Ibc.LightClients.SoloMachine
{
    public record HeaderV1(
        ulong Sequence,
        ulong Timestamp,
        byte[] Signature,
        IPublicKey NewPublicKey,
        string NewDiversifier) : IHeader
    {
        public Serialization.LightClients.IHeader ToSerialization()
        {
            return new Serialization.LightClients.SoloMachine.HeaderV1(
                this.Sequence,
                this.Timestamp,
                this.Signature,
                this.NewPublicKey.ToProto(),
                this.NewDiversifier);
        }
    }
}
