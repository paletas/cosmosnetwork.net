using CosmosNetwork.Tendermint.Types.Version;

namespace CosmosNetwork.Tendermint.Types
{
    public record Header(
        Consensus Version,
        string ChainId,
        ulong Height,
        DateTime Time,
        BlockId LastBlockId,
        byte[] LastCommitHash,
        byte[] DataHash,
        byte[] ValidatorsHash,
        byte[] NextValidatorsHash,
        byte[] ConsensusHash,
        byte[] AppHash,
        byte[] LastResultsHash,
        byte[] EvidenceHash,
        byte[] ProposerAddress)
    {
        public Serialization.Header ToSerialization()
        {
            return new Serialization.Header(
                this.Version.ToSerialization(),
                this.ChainId,
                this.Height,
                this.Time,
                this.LastBlockId.ToSerialization(),
                this.LastCommitHash,
                this.DataHash,
                this.ValidatorsHash,
                this.NextValidatorsHash,
                this.ConsensusHash,
                this.AppHash,
                this.LastResultsHash,
                this.EvidenceHash,
                this.ProposerAddress);
        }
    }
}
