using ProtoBuf;

namespace CosmosNetwork.Confio.Serialization
{
    [ProtoContract]
    public record LeafOp(
        [property: ProtoMember(1, Name = "hash")] HashOperationEnum Hash,
        [property: ProtoMember(2, Name = "prehash_key")] HashOperationEnum PreHashKey,
        [property: ProtoMember(3, Name = "prehash_value")] HashOperationEnum PreHashValue,
        [property: ProtoMember(4, Name = "length")] LengthOperationEnum Length,
        [property: ProtoMember(5, Name = "prefix")] byte[] Prefix)
    {
        public Confio.LeafOp ToModel()
        {
            return new Confio.LeafOp(
                (Confio.HashOperationEnum)this.Hash,
                (Confio.HashOperationEnum)this.PreHashKey,
                (Confio.HashOperationEnum)this.PreHashValue,
                (Confio.LengthOperationEnum)this.Length,
                this.Prefix);
        }
    }
}
