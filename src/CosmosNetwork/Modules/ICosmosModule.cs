namespace CosmosNetwork.Modules
{
    public interface ICosmosModule
    {
        void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry);
    }
}
