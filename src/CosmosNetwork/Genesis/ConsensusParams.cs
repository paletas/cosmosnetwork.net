
using CosmosNetwork.Keys;

namespace CosmosNetwork.Genesis
{
    public record ConsensusParams(
        ulong BlockMaxBytes,
        ulong BlockMaxGas,
        ulong BlockTimeIotaInMs,
        ulong EvidenceMaxAgeNumBlocks,
        UInt128 EvidenceMaxAgeDuration,
        ulong EvidenceMaxBytes,
        KeyCurveAlgorithm[] ValidatorKeyTypes);
}
