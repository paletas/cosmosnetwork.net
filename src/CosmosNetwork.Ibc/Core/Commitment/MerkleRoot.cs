namespace CosmosNetwork.Ibc.Core.Commitment
{
    public record MerkleRoot(byte[] Hash)
    {
        internal Serialization.Core.Commitment.MerkleRoot ToSerialization()
        {
            return new Serialization.Core.Commitment.MerkleRoot(this.Hash);
        }
    }
}
