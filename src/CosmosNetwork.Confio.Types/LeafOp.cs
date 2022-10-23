namespace CosmosNetwork.Confio.Types
{
    public record LeafOp(
        HashOperationEnum Hash,
        HashOperationEnum PreHashKey,
        HashOperationEnum PreHashValue,
        LengthOperationEnum Length,
        byte[] Prefix)
    {
        internal Serialization.LeafOp ToSerialization()
        {
            return new Serialization.LeafOp(
                (Serialization.HashOperationEnum)this.Hash,
                (Serialization.HashOperationEnum)this.PreHashKey,
                (Serialization.HashOperationEnum)this.PreHashValue,
                (Serialization.LengthOperationEnum)this.Length,
                this.Prefix);
        }
    }
}
