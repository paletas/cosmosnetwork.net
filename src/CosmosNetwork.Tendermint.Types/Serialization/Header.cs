using CosmosNetwork.Tendermint.Types.Serialization.Version;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public record Header(
        [property: ProtoMember(1, Name = "version")] Consensus Version,
        [property: ProtoMember(2, Name = "chain_id")] string ChainId,
        [property: ProtoMember(3, Name = "height")] ulong Height,
        [property: ProtoMember(5, Name = "last_block_id")] BlockId LastBlockId,
        [property: ProtoMember(6, Name = "last_commit_hash")] byte[] LastCommitHash,
        [property: ProtoMember(7, Name = "data_hash")] byte[] DataHash,
        [property: ProtoMember(8, Name = "validators_hash")] byte[] ValidatorsHash,
        [property: ProtoMember(9, Name = "next_validators_hash")] byte[] NextValidatorsHash,
        [property: ProtoMember(10, Name = "consensus_hash")] byte[] ConsensusHash,
        [property: ProtoMember(11, Name = "app_hash")] byte[] AppHash,
        [property: ProtoMember(12, Name = "last_results_hash")] byte[] LastResultsHash,
        [property: ProtoMember(13, Name = "evidence_hash")] byte[] EvidenceHash,
        [property: ProtoMember(14, Name = "proposer_address")] byte[] ProposerAddress)
    {
        [ProtoMember(4, Name = "time"), JsonIgnore]
        public ProtoBuf.WellKnownTypes.Timestamp TimeProto
        {
            get { return new ProtoBuf.WellKnownTypes.Timestamp(this.Time); }
            set { this.Time = value.AsDateTime(); }
        }

        [JsonPropertyName("time"), ProtoIgnore]
        public DateTime Time { get; set; }

        public Types.Header ToModel()
        {
            return new Types.Header(
                this.Version.ToModel(),
                this.ChainId,
                this.Height,
                this.Time,
                this.LastBlockId.ToModel(),
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
