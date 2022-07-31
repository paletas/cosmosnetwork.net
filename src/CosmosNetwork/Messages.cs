namespace CosmosNetwork
{
    public abstract record Message()
    {
        internal abstract Serialization.SerializerMessage ToJson();
    }
}