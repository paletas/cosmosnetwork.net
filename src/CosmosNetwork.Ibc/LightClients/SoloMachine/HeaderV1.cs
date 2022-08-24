namespace CosmosNetwork.Ibc.LightClients.SoloMachine
{
    public record HeaderV1(
        ulong Sequence,
        ulong Timestamp,
        byte[] Signature,
        SignatureKey NewPublicKey,
        string NewDiversifier) : IHeader
    {
        public Serialization.LightClients.IHeader ToSerialization()
        {
            return new Serialization.LightClients.SoloMachine.HeaderV1(
                this.Sequence,
                this.Timestamp,
                this.Signature,
                this.NewPublicKey.ToSerialization().AsTendermint(),
                this.NewDiversifier);
        }
    }
}
