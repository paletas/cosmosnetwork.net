using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.LightClients.SoloMachine
{
    [ProtoContract]
    internal record HeaderV1(
        [property: ProtoMember(1, Name = "sequence")] ulong Sequence,
        [property: ProtoMember(1, Name = "timestamp")] ulong Timestamp,
        [property: ProtoMember(1, Name = "signature")] byte[] Signature,
        [property: ProtoMember(1, Name = "new_public_key")] CosmosNetwork.Serialization.Tendermint.PublicKey NewPublicKey,
        [property: ProtoMember(1, Name = "new_diversifier")] string NewDiversifier) : IHeader
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
