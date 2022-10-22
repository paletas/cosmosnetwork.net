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
                (Serialization.HashOperationEnum)Hash,
                (Serialization.HashOperationEnum)PreHashKey,
                (Serialization.HashOperationEnum)PreHashValue,
                (Serialization.LengthOperationEnum)Length,
                Prefix);
        }
    }
}
