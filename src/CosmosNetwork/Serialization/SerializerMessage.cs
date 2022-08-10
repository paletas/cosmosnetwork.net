namespace CosmosNetwork.Serialization
{
    public abstract record SerializerMessage()
    {
        internal protected abstract CosmosNetwork.Message ToModel();
    };
}
