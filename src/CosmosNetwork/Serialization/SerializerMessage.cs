namespace CosmosNetwork.Serialization
{
    public abstract record SerializerMessage()
    {
        protected internal abstract CosmosNetwork.Message ToModel();
    };
}
