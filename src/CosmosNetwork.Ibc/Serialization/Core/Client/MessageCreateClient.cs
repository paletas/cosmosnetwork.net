using CosmosNetwork.Ibc.Serialization.LightClients;
using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Client
{
    [ProtoContract]
    internal record MessageCreateClient(
        [property: ProtoMember(3, Name = "signer")] string Signer) : SerializerMessage(Ibc.Core.Client.MessageCreateClient.COSMOS_DESCRIPTOR)
    {
        [ProtoIgnore]
        public IClientState ClientState { get; set; } = null!;

        [ProtoMember(1, Name = "client_state")]
        public Any ClientStatePacked
        {
            get => Any.Pack(this.ClientState);
            set => this.ClientState = value.UnpackClientState();
        }

        [ProtoIgnore]
        public IConsensusState ConsensusState { get; set; } = null!;

        [ProtoMember(2, Name = "consensus_state")]
        public Any ConsensusStatePacked
        {
            get => Any.Pack(this.ConsensusState);
            set => this.ConsensusState = value.UnpackConsensusState();
        }

        protected override Message ToModel()
        {
            return new Ibc.Core.Client.MessageCreateClient(
                this.ClientState.ToModel(),
                this.ConsensusState.ToModel(),
                this.Signer);
        }
    }
}
