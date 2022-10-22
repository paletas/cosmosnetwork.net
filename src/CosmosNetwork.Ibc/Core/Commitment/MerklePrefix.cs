namespace CosmosNetwork.Ibc.Core.Commitment
{
    public record MerklePrefix(byte[] KeyPrefix)
    {
        internal Serialization.Core.Commitment.MerklePrefix ToSerialization()
        {
            return new Serialization.Core.Commitment.MerklePrefix(this.KeyPrefix);
        }
    }
}
