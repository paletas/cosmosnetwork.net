using CosmosNetwork.Serialization.Proto;
using ProtoBuf;
using ProtoBuf.WellKnownTypes;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.LightClients.SoloMachine
{
    [ProtoContract]
    internal record ConsensusStateV1(
        [property: ProtoMember(2, Name = "diversifier")] string Diversifier) : IConsensusState
    {
        public const string AnyType = "/ibc.lightclients.solomachine.v1.ConsensusState";

        public string TypeUrl => AnyType;

        [property: ProtoMember(3, Name = "timestamp"), JsonIgnore]
        public Timestamp Timestamp { get; set; }

        [JsonPropertyName("timestamp"), ProtoIgnore]
        public DateTime TimestampDateTime
        {
            get { return this.Timestamp.AsDateTime(); }
            set { this.Timestamp = new Timestamp(value); }
        }

        [ProtoIgnore]
        public CosmosNetwork.Serialization.Proto.PublicKey PublicKey { get; set; } = null!;

        [ProtoMember(1, Name = "public_key")]
        public Any PublicKeyPacked
        {
            get => Any.Pack(this.PublicKey);
            set => value.Unpack<CosmosNetwork.Serialization.Proto.PublicKey>();
        }

        public Ibc.LightClients.IConsensusState ToModel()
        {
            return new Ibc.LightClients.SoloMachine.ConsensusState(
                this.PublicKey.ToModel(),
                this.Diversifier,
                this.Timestamp);
        }
    }
}
