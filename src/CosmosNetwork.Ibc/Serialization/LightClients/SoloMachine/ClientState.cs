using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.LightClients.SoloMachine
{
    [ProtoContract]
    internal record ClientState(
        [property: ProtoMember(1, Name = "sequence")] ulong Sequence,
        [property: ProtoMember(2, Name = "is_frozen")] bool IsFrozen,
        [property: ProtoMember(3, Name = "consensus_state")] IConsensusState ConsensusState,
        [property: ProtoMember(4, Name = "allow_update_after_proposal")] bool AllowUpdateAfterProposal) : IClientState
    {
        public const string AnyType = "/ibc.lightclients.solomachine.v2.ClientState";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IClientState ToModel()
        {
            return new Ibc.LightClients.SoloMachine.ClientState(
                this.Sequence,
                this.IsFrozen,
                this.ConsensusState.ToModel(),
                this.AllowUpdateAfterProposal);
        }
    }
}
