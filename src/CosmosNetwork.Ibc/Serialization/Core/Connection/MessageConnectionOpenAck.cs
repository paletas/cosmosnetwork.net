using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Ibc.Serialization.LightClients;
using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Connection
{
    [ProtoContract]
    internal record MessageConnectionOpenAck(
        [property: ProtoMember(1, Name = "client_id")] string ConnectionId,
        [property: ProtoMember(2, Name = "previous_connection_id")] string PreviousConnectionId,
        [property: ProtoMember(3, Name = "version")] Version Version,
        [property: ProtoMember(5, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(6, Name = "proof_init")] byte[] ProofTry,
        [property: ProtoMember(7, Name = "proof_client")] byte[] ProofClient,
        [property: ProtoMember(8, Name = "proof_consensus")] byte[] ProofConsensus,
        [property: ProtoMember(9, Name = "consensus_height")] Height ConsensusHeight,
        [property: ProtoMember(10, Name = "signer")] string Signer) : SerializerMessage
    {
        [ProtoIgnore]
        public IClientState ClientState { get; set; } = null!;

        [ProtoMember(4, Name = "client_state")]
        public Any ClientStatePacked
        {
            get => Any.Pack(this.ClientState);
            set => this.ClientState = value.UnpackClientState();
        }

        protected override Message ToModel()
        {
            return new Ibc.Core.Connection.MessageConnectionOpenAck(
                this.ConnectionId,
                this.PreviousConnectionId,
                this.Version.ToModel(),
                this.ClientState.ToModel(),
                this.ProofHeight.ToModel(),
                this.ProofTry,
                this.ProofClient,
                this.ProofConsensus,
                this.ConsensusHeight.ToModel(),
                this.Signer);
        }
    }
}
