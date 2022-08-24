using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Client
{
    [ProtoContract]
    internal record MessageCreateClient(
        [property: ProtoMember(3, Name = "signer")] string Signer) : SerializerMessage
    {
        [ProtoIgnore]
        public byte[] ClientState { get; set; } = null!;

        [ProtoMember(1, Name = "client_state")]
        public Any ClientStatePacked
        {
            get => Any.Pack(null, this.ClientState);
            set => this.ClientState = value.ToArray();
        }

        [ProtoIgnore]
        public byte[] ConsensusState { get; set; } = null!;

        [ProtoMember(2, Name = "consensus_state")]
        public Any ConsensusStatePacked
        {
            get => Any.Pack(null, this.ConsensusState);
            set => this.ConsensusState = value.ToArray();
        }

        protected override Message ToModel()
        {
            return new Ibc.Core.Client.MessageCreateClient(
                this.ClientState,
                this.ConsensusState,
                this.Signer);
        }
    }
}
