using ProtoBuf;
using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public record CommitSig(
        [property: ProtoMember(1, Name = "block_id_flag")] BlockIdFlagEnum BlockIdFlag,
        [property: ProtoMember(2, Name = "validator_address")] byte[] ValidatorAddress,
        [property: ProtoMember(3, Name = "timestamp")] Timestamp Timestamp,
        [property: ProtoMember(4, Name = "signature")] byte[] Signature)
    {
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
