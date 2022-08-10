namespace CosmosNetwork
{
    public abstract record Message()
    {
        internal protected abstract Serialization.SerializerMessage ToJson();
    }
}