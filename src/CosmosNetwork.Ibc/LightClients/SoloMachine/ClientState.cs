namespace CosmosNetwork.Ibc.LightClients.SoloMachine
{
    public record ClientState(
        ulong Sequence,
        bool IsFrozen,
        IConsensusState ConsensusState,
        bool AllowUpdateAfterProposal) : IClientState
    {
        public Serialization.LightClients.IClientState ToSerialization()
        {
            return new Serialization.LightClients.SoloMachine.ClientState(
                this.Sequence,
                this.IsFrozen,
                this.ConsensusState.ToSerialization(),
                this.AllowUpdateAfterProposal);
        }
    }
}
