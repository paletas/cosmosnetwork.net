using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Slashing
{
    public class SlashingModule : ICosmosModule
    {
        public void ConfigureModule(CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageUnjail, Serialization.MessageUnjail>();
        }
    }
}
