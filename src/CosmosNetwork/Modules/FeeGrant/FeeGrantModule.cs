using CosmosNetwork.Modules.FeeGrant.Serialization.Allowances;
using CosmosNetwork.Modules.FeeGrant.Serialization.Allowances.Json;

namespace CosmosNetwork.Modules.FeeGrant
{
    public class FeeGrantModule : ICosmosMessageModule
    {
        public FeeGrantModule()
        {
            this.AllowancesRegistry = new AllowancesRegistry();
        }

        public AllowancesRegistry AllowancesRegistry { get; init; }

        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {            
            cosmosOptions.JsonSerializerOptions.Converters.Add(new AllowanceConverter(this.AllowancesRegistry));
            cosmosOptions.JsonSerializerOptions.Converters.Add(new AllowancesConverter(this.AllowancesRegistry));

            messageRegistry.RegisterMessage<MessageGrantAllowance, Serialization.MessageGrantAllowance>(MessageGrantAllowance.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageRevokeAllowance, Serialization.MessageRevokeAllowance>(MessageRevokeAllowance.COSMOS_DESCRIPTOR);

            this.AllowancesRegistry.Register<BasicAllowance>(BasicAllowance.AllowanceType);
            this.AllowancesRegistry.Register<PeriodicAllowance>(PeriodicAllowance.AllowanceType);
            this.AllowancesRegistry.Register<AllowedMessageAllowance>(AllowedMessageAllowance.AllowanceType);
        }
    }
}
