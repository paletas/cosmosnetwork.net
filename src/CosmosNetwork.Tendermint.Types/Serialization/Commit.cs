using ProtoBuf;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public record Commit(
        [property: ProtoMember(1, Name = "height")] long Height,
        [property: ProtoMember(2, Name = "round")] int Round,
        [property: ProtoMember(3, Name = "block_id")] BlockId BlockId,
        [property: ProtoMember(4, Name = "signatures")] CommitSig[] Signatures)
    {
        public Types.Commit ToModel()
        {
            return new Types.Commit(
                this.Height,
                this.Round,
                this.BlockId.ToModel(),
                this.Signatures.Select(sig => sig.ToModel()).ToArray());
        }
    }
}
