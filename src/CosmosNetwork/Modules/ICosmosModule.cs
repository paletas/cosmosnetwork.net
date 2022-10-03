namespace CosmosNetwork.Modules
{
    public interface ICosmosMessageModule
    {
        void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry);
    }
}
