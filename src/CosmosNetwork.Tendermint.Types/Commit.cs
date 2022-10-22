namespace CosmosNetwork.Tendermint.Types
{
    public record Commit(
        long Height,
        int Round,
        BlockId BlockId,
        CommitSignature[] Signatures)
    {
        public Serialization.Commit ToSerialization()
        {
            return new Serialization.Commit(
                this.Height,
                this.Round,
                this.BlockId.ToSerialization(),
                this.Signatures.Select(sig => sig.ToSerialization()).ToArray());
        }
    }
}
