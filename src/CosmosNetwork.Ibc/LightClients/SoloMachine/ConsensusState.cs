namespace CosmosNetwork.Ibc.LightClients.SoloMachine
{
    public record ConsensusState(
        SignatureKey PublicKey,
        string Diversifier,
        ulong Timestamp) : IConsensusState
    {
        public Serialization.LightClients.IConsensusState ToSerialization()
        {
            return new Serialization.LightClients.SoloMachine.ConsensusState(
                this.Diversifier,
                this.Timestamp)
            {
                PublicKey = this.PublicKey.ToSerialization().AsTendermint()
            };
        }
    }
}
