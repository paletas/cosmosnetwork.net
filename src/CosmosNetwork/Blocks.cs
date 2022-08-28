namespace CosmosNetwork
{
    public record Block(BlockIdentification? BlockId, BlockDetails Details);

    public record BlockIdentification(string Hash, BlockIdentificationParts Parts);

    public record BlockIdentificationParts(long Total, string Hash);

    public record BlockDetails(BlockHeader Header, BlockData Data, BlockCommit? LastCommit);

    public record BlockHeader
    (
        BlockVersion Version,
        string ChainId,
        ulong Height,
        DateTime Time,
        BlockIdentification LastBlock,
        string LastCommitHash,
        string DataHash,
        string ValidatorsHash,
        string NextValidatorsHash,
        string ConsensusHash,
        string AppHash,
        string LastResultsHash,
        string EvidenceHash,
        string ProposerAddress
    );

    public record BlockVersion(uint Block);

    public record BlockData(string[] Transactions);

    public record BlockEvidence();

    public record BlockCommit(ulong Height, uint Round, BlockIdentification BlockId, BlockSignature[] Signatures);

    public record BlockSignature(BlockFlagEnum BlockFlag, string ValidatorAddress, DateTime Timestamp, string Signature);

    public enum BlockFlagEnum
    {
        Unknown = 1,
        Absent = 2,
        Commit = 3,
        Nil = 4
    }
}
