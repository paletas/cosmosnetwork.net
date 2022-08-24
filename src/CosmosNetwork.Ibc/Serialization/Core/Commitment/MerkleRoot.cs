using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Commitment
{
    [ProtoContract]
    internal record MerkleRoot(
        [property: ProtoMember(1, Name = "hash")] byte[] Hash)
    {
        public Ibc.Core.Commitment.MerkleRoot ToModel()
        {
            return new Ibc.Core.Commitment.MerkleRoot(this.Hash);
        }
    }
}
