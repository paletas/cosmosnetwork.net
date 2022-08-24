namespace CosmosNetwork.Ibc.Serialization.LightClients.SoloMachine
{
    internal record HeaderV1(
        ulong Sequence,
        ulong Timestamp,
        byte[] Signature,
        CosmosNetwork.Serialization.Tendermint.PublicKey NewPublicKey,
        string NewDiversifier) : IHeader
    {
        public const string AnyType = "/ibc.lightclients.solomachine.v1.Header";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IHeader ToModel()
        {
            return new Ibc.LightClients.SoloMachine.HeaderV1(
                this.Sequence,
                this.Timestamp,
                this.Signature,
                this.NewPublicKey.ToModel(),
                this.NewDiversifier);
        }
    }
}
