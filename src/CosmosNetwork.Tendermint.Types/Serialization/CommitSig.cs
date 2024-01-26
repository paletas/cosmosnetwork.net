using ProtoBuf;
using ProtoBuf.WellKnownTypes;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public record CommitSig(
        [property: ProtoMember(1, Name = "block_id_flag")] BlockIdFlagEnum BlockIdFlag,
        [property: ProtoMember(2, Name = "validator_address")] byte[] ValidatorAddress,
        [property: ProtoMember(4, Name = "signature")] byte[] Signature)
    {
        [ProtoMember(3, Name = "timestamp"), JsonIgnore]
        public Timestamp TimestampProto
        {
            get { return new Timestamp(this.Timestamp); }
            set { this.Timestamp = value.AsDateTime(); }
        }

        [JsonPropertyName("timestamp"), ProtoIgnore]
        public DateTime Timestamp { get; set; }

        public Types.CommitSignature ToModel()
        {
            return new Types.CommitSignature(
                (Types.BlockIdFlagEnum)this.BlockIdFlag,
                this.ValidatorAddress,
                this.Timestamp,
                this.Signature);
        }
    }
}
