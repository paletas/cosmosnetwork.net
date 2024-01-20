using CosmosNetwork.Ibc.Serialization.Core.Commitment;
using ProtoBuf;
using ProtoBuf.WellKnownTypes;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.LightClients.Tendermint
{
    [ProtoContract]
    internal record ConsensusState(
        [property: ProtoMember(2, Name = "root")] MerkleRoot Root,
        [property: ProtoMember(3, Name = "next_validators_hash")] byte[] NextValidatorsHash) : IConsensusState
    {
        public const string AnyType = "/ibc.lightclients.tendermint.v1.ConsensusState";

        public string TypeUrl => AnyType;

        [property: ProtoMember(1, Name = "timestamp"), JsonIgnore]
        public Timestamp Timestamp { get; set; }

        [JsonPropertyName("timestamp"), ProtoIgnore]
        public DateTime TimestampDateTime
        {
            get { return this.Timestamp; }
            set { this.Timestamp = value; }
        }

        public Ibc.LightClients.IConsensusState ToModel()
        {
            return new Ibc.LightClients.Tendermint.ConsensusState(
                this.Timestamp,
                this.Root.ToModel(),
                this.NextValidatorsHash);
        }
    }
}
