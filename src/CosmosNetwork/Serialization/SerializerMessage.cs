namespace CosmosNetwork.Serialization
{
    internal abstract record SerializerMessage()
    {
        internal abstract CosmosNetwork.Message ToModel();
    };
}
