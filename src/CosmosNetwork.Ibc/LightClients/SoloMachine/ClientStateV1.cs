namespace CosmosNetwork.Ibc.LightClients.SoloMachine
{
    public record ClientStateV1(
        ulong Sequence,
        ulong FrozenSequence,
        IConsensusState ConsensusState,
        bool AllowUpdateAfterProposal) : IClientState
    {
        public Serialization.LightClients.IClientState ToSerialization()
        {
            return new Serialization.LightClients.SoloMachine.ClientStateV1(
                this.Sequence,
                this.FrozenSequence,
                this.ConsensusState.ToSerialization(),
                this.AllowUpdateAfterProposal);
        }
    }
}
