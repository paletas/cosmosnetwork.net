using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Commitment
{
    [ProtoContract]
    internal record MerklePrefix(
        [property: ProtoMember(1, Name = "key_prefix")] byte[] KeyPrefix)
    {
        public Ibc.Core.Commitment.MerklePrefix ToModel()
        {
            return new Ibc.Core.Commitment.MerklePrefix(this.KeyPrefix);
        }
    }
}
