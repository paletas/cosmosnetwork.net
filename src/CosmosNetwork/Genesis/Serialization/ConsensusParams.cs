using CosmosNetwork.Keys;

namespace CosmosNetwork.Genesis.Serialization
{
    internal class ConsensusParams
    {
        public ConsensusBlockParams Block { get; set; }

        public ConsensusEvidenceParams Evidence { get; set; }

        public ConsensusValidatorParams Validator { get; set; }
        
        public Genesis.ConsensusParams ToModel()
        {
            return new Genesis.ConsensusParams(
                this.Block.MaxBytes,
                this.Block.MaxGas,
                this.Block.TimeIotaMs,
                this.Evidence.MaxAgeNumBlocks,
                this.Evidence.MaxAgeDuration,
                this.Evidence.MaxBytes,
                this.Validator.PubKeyTypes.Select(k => (KeyCurveAlgorithm)k).ToArray());
        }

        internal class ConsensusBlockParams
        {
            public ulong MaxBytes { get; set; }

            public ulong MaxGas { get; set; }

            public ulong TimeIotaMs { get; set; }
        }

        internal class ConsensusEvidenceParams
        {
            public UInt128 MaxAgeDuration { get; set; }

            public ulong MaxAgeNumBlocks { get; set; }

            public ulong MaxBytes { get; set; }
        }

        internal class ConsensusValidatorParams
        {
            public PublicKeyTypes[] PubKeyTypes { get; set; }
        }
    }
}
