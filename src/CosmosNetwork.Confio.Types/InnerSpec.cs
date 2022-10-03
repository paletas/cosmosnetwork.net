namespace CosmosNetwork.Confio.Types
{
    public record InnerSpec(
        int[] ChildOrder,
        int ChildSize,
        int MinPrefixLength,
        int MaxPrefixLength,
        byte[] EmptyChild,
        HashOperationEnum Hash)
    {
        internal Serialization.InnerSpec ToSerialization()
        {
            return new Serialization.InnerSpec(
                ChildOrder,
                ChildSize,
                MinPrefixLength,
                MaxPrefixLength,
                EmptyChild,
                (Serialization.HashOperationEnum)Hash);
        }
    }
}
