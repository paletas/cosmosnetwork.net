using CosmosNetwork.Ibc.Core.Commitment;

namespace CosmosNetwork.Ibc.LightClients.Tendermint
{
    public record ConsensusState(
        DateTime Timestamp,
        MerkleRoot Root,
        byte[] NextValidatorsHash) : IConsensusState
    {
        public Serialization.LightClients.IConsensusState ToSerialization()
        {
            return new Serialization.LightClients.Tendermint.ConsensusState(
                this.Timestamp,
                this.Root.ToSerialization(),
                this.NextValidatorsHash);
        }
    }
}
