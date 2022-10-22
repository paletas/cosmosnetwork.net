using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json
{
    internal record Block(BlockIdentification? BlockId, [property: JsonPropertyName("block")] BlockDetails Details)
    {
        internal CosmosNetwork.Block ToModel()
        {
            return new CosmosNetwork.Block(BlockId?.ToModel(), Details.ToModel());
        }
    }

    internal record BlockIdentification(string Hash, [property: JsonPropertyName("part_set_header")] BlockIdentificationParts Parts)
    {
        internal CosmosNetwork.BlockIdentification ToModel()
        {
            return new CosmosNetwork.BlockIdentification(Hash, Parts.ToModel());
        }
    }

    internal record BlockIdentificationParts(long Total, string Hash)
    {
        internal CosmosNetwork.BlockIdentificationParts ToModel()
        {
            return new CosmosNetwork.BlockIdentificationParts(Total, Hash);
        }
    }

    internal record BlockDetails(BlockHeader Header, BlockData Data, BlockCommit? LastCommit)
    {
        internal CosmosNetwork.BlockDetails ToModel()
        {
            return new CosmosNetwork.BlockDetails(Header.ToModel(), Data.ToModel(), LastCommit?.ToModel());
        }
    }

    internal record BlockHeader
    (
        BlockVersion Version,
        string ChainId,
        ulong Height,
        DateTime Time,
        [property: JsonPropertyName("last_block_id")] BlockIdentification LastBlock,
        string LastCommitHash,
        string DataHash,
        string ValidatorsHash,
        string NextValidatorsHash,
        string ConsensusHash,
        string AppHash,
        string LastResultsHash,
        string EvidenceHash,
        string ProposerAddress
    )
    {
        internal CosmosNetwork.BlockHeader ToModel()
        {
            return new CosmosNetwork.BlockHeader
            (
                Version.ToModel(),
                ChainId,
                Height,
                Time,
                LastBlock.ToModel(),
                LastCommitHash,
                DataHash,
                ValidatorsHash,
                NextValidatorsHash,
                ConsensusHash,
                AppHash,
                LastResultsHash,
                EvidenceHash,
                ProposerAddress
            );
        }
    }

    internal record BlockVersion(uint Block)
    {
        internal CosmosNetwork.BlockVersion ToModel()
        {
            return new CosmosNetwork.BlockVersion(Block);
        }
    }

    internal record BlockData([property: JsonPropertyName("txs")] string[] Transactions)
    {
        internal CosmosNetwork.BlockData ToModel()
        {
            return new CosmosNetwork.BlockData(Transactions);
        }
    }

    internal record BlockCommit(ulong Height, uint Round, BlockIdentification BlockId, BlockSignature[] Signatures)
    {
        internal CosmosNetwork.BlockCommit ToModel()
        {
            return new CosmosNetwork.BlockCommit(Height, Round, BlockId.ToModel(), Signatures.Select(sig => sig.ToModel()).ToArray());
        }
    }

    internal record BlockSignature(BlockFlagEnum BlockIdFlag, string ValidatorAddress, DateTime Timestamp, string Signature)
    {
        internal CosmosNetwork.BlockSignature ToModel()
        {
            return new CosmosNetwork.BlockSignature((CosmosNetwork.BlockFlagEnum)BlockIdFlag, ValidatorAddress, Timestamp, Signature);
        }
    }

    internal enum BlockFlagEnum
    {
        [EnumMember(Value = "BLOCK_ID_FLAG_UNKNOWN")]
        Unknown = 1,

        [EnumMember(Value = "BLOCK_ID_FLAG_ABSENT")]
        Absent = 2,

        [EnumMember(Value = "BLOCK_ID_FLAG_COMMIT")]
        Commit = 3,

        [EnumMember(Value = "BLOCK_ID_FLAG_NIL")]
        Nil = 4
    }
}
