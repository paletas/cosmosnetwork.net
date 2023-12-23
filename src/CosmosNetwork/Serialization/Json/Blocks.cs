using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json
{
    internal record Block(BlockIdentification? BlockId, [property: JsonPropertyName("block")] BlockDetails Details)
    {
        internal CosmosNetwork.Block ToModel()
        {
            return new CosmosNetwork.Block(this.BlockId?.ToModel(), this.Details.ToModel());
        }
    }

    internal record BlockIdentification(string Hash, [property: JsonPropertyName("part_set_header")] BlockIdentificationParts Parts)
    {
        internal CosmosNetwork.BlockIdentification ToModel()
        {
            return new CosmosNetwork.BlockIdentification(this.Hash, this.Parts.ToModel());
        }
    }

    internal record BlockIdentificationParts(long Total, string Hash)
    {
        internal CosmosNetwork.BlockIdentificationParts ToModel()
        {
            return new CosmosNetwork.BlockIdentificationParts(this.Total, this.Hash);
        }
    }

    internal record BlockDetails(BlockHeader Header, BlockData Data, BlockCommit? LastCommit)
    {
        internal CosmosNetwork.BlockDetails ToModel()
        {
            return new CosmosNetwork.BlockDetails(this.Header.ToModel(), this.Data.ToModel(), this.LastCommit?.ToModel());
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
                this.Version.ToModel(),
                this.ChainId,
                this.Height,
                this.Time,
                this.LastBlock.ToModel(),
                this.LastCommitHash,
                this.DataHash,
                this.ValidatorsHash,
                this.NextValidatorsHash,
                this.ConsensusHash,
                this.AppHash,
                this.LastResultsHash,
                this.EvidenceHash,
                this.ProposerAddress
            );
        }
    }

    internal record BlockVersion(uint Block)
    {
        internal CosmosNetwork.BlockVersion ToModel()
        {
            return new CosmosNetwork.BlockVersion(this.Block);
        }
    }

    internal record BlockData([property: JsonPropertyName("txs")] string[] Transactions)
    {
        internal CosmosNetwork.BlockData ToModel()
        {
            return new CosmosNetwork.BlockData(this.Transactions);
        }
    }

    internal record BlockCommit(ulong Height, uint Round, BlockIdentification BlockId, BlockSignature[] Signatures)
    {
        internal CosmosNetwork.BlockCommit ToModel()
        {
            return new CosmosNetwork.BlockCommit(this.Height, this.Round, this.BlockId.ToModel(), this.Signatures.Select(sig => sig.ToModel()).ToArray());
        }
    }

    internal record BlockSignature(BlockFlagEnum BlockIdFlag, string ValidatorAddress, DateTime Timestamp, string Signature)
    {
        internal CosmosNetwork.BlockSignature ToModel()
        {
            return new CosmosNetwork.BlockSignature((CosmosNetwork.BlockFlagEnum)this.BlockIdFlag, this.ValidatorAddress, this.Timestamp, this.Signature);
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
