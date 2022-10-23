namespace CosmosNetwork.Modules.Slashing
{
    public class SlashingModule : ICosmosMessageModule
    {
        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageUnjail, Serialization.MessageUnjail>();
        }
    }
}
