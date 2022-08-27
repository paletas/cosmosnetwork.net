using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.LightClients.SoloMachine
{
    [ProtoContract]
    internal record Header(
        [property: ProtoMember(1, Name = "sequence")] ulong Sequence,
        [property: ProtoMember(2, Name = "timestamp")] ulong Timestamp,
        [property: ProtoMember(3, Name = "signature")] byte[] Signature,
        [property: ProtoMember(4, Name = "new_public_key")] CosmosNetwork.Serialization.Tendermint.PublicKey NewPublicKey,
        [property: ProtoMember(5, Name = "new_diversifier")] string NewDiversifier) : IHeader
    {
        public const string AnyType = "/ibc.lightclients.solomachine.v2.Header";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IHeader ToModel()
        {
            return new Ibc.LightClients.SoloMachine.Header(
                this.Sequence,
                this.Timestamp,
                this.Signature,
                this.NewPublicKey.ToModel(),
                this.NewDiversifier);
        }
    }
}
