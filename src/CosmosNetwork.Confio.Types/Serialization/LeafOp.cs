﻿using ProtoBuf;

namespace CosmosNetwork.Confio.Types.Serialization
{
    [ProtoContract]
    public record LeafOp(
        [property: ProtoMember(1, Name = "hash")] HashOperationEnum Hash,
        [property: ProtoMember(2, Name = "prehash_key")] HashOperationEnum PreHashKey,
        [property: ProtoMember(3, Name = "prehash_value")] HashOperationEnum PreHashValue,
        [property: ProtoMember(4, Name = "length")] LengthOperationEnum Length,
        [property: ProtoMember(5, Name = "prefix")] byte[] Prefix)
    {
        public Types.LeafOp ToModel()
        {
            return new Types.LeafOp(
                (Types.HashOperationEnum)this.Hash,
                (Types.HashOperationEnum)this.PreHashKey,
                (Types.HashOperationEnum)this.PreHashValue,
                (Types.LengthOperationEnum)this.Length,
                this.Prefix);
        }
    }
}
