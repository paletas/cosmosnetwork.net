using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.LightClients.SoloMachine
{
    [ProtoContract]
    internal record ClientStateV1(
        [property: ProtoMember(1, Name = "sequence")] ulong Sequence,
        [property: ProtoMember(2, Name = "frozen_sequence")] ulong FrozenSequence,
        [property: ProtoMember(3, Name = "consensus_state")] IConsensusState ConsensusState,
        [property: ProtoMember(4, Name = "allow_update_after_proposal")] bool AllowUpdateAfterProposal) : IClientState
    {
        public const string AnyType = "/ibc.lightclients.solomachine.v1.ClientState";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IClientState ToModel()
        {
            return new Ibc.LightClients.SoloMachine.ClientStateV1(
                this.Sequence,
                this.FrozenSequence,
                this.ConsensusState.ToModel(),
                this.AllowUpdateAfterProposal);
        }
    }
}
