namespace CosmosNetwork.Ibc.LightClients.SoloMachine
{
    public record ConsensusState(
        IPublicKey PublicKey,
        string Diversifier,
        DateTime Timestamp) : IConsensusState
    {
        public Serialization.LightClients.IConsensusState ToSerialization()
        {
            return new Serialization.LightClients.SoloMachine.ConsensusState(this.Diversifier)
            {
                TimestampDateTime = this.Timestamp,
                PublicKey = this.PublicKey.ToJson().ToProto()
            };
        }
    }
}
