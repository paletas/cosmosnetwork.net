namespace CosmosNetwork
{
    public abstract record Message()
    {
        protected internal abstract Serialization.SerializerMessage ToSerialization();
    }
}