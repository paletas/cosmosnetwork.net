using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Connection
{
    [ProtoContract]
    internal record MessageConnectionOpenTry(
        [property: ProtoMember(1, Name = "client_id")] string ClientId,
        [property: ProtoMember(2, Name = "previous_connection_id")] string PreviousConnectionId,
        [property: ProtoMember(4, Name = "counterparty")] Counterparty Counterparty,
        [property: ProtoMember(5, Name = "delay_period")] ulong DelayPeriod,
        [property: ProtoMember(6, Name = "counterparty_versions")] Version[] CounterpartyVersions,
        [property: ProtoMember(7, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(8, Name = "proof_init")] byte[] ProofInit,
        [property: ProtoMember(9, Name = "proof_client")] byte[] ProofClient,
        [property: ProtoMember(10, Name = "proof_consensus")] byte[] ProofConsensus,
        [property: ProtoMember(11, Name = "consensus_height")] Height ConsensusHeight,
        [property: ProtoMember(12, Name = "signer")] string Signer) : SerializerMessage
    {
        [ProtoIgnore]
        public byte[] ClientState { get; set; } = null!;

        [ProtoMember(3, Name = "client_state")]
        public Any ClientStatePacked
        {
            get => Any.Pack(null, this.ClientState);
            set => this.ClientState = value.ToArray();
        }

        protected override Message ToModel()
        {
            return new Ibc.Core.Connection.MessageConnectionOpenTry(
                this.ClientId,
                this.PreviousConnectionId,
                this.ClientState,
                this.Counterparty.ToModel(),
                this.DelayPeriod,
                this.CounterpartyVersions.Select(cv => cv.ToModel()).ToArray(),
                this.ProofHeight.ToModel(),
                this.ProofInit,
                this.ProofClient,
                this.ProofConsensus,
                this.ConsensusHeight.ToModel(),
                this.Signer);
        }
    }
}
