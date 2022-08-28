using CosmosNetwork.Modules.FeeGrant.Allowances;
using CosmosNetwork.Modules.FeeGrant.Serialization.Allowances;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.FeeGrant
{
    public class FeeGrantModule : ICosmosModule
    {
        private readonly IServiceCollection _services;

        public FeeGrantModule(IServiceCollection services)
        {
            this._services = services;

            this.AllowancesRegistry = new AllowancesRegistry();
        }

        public AllowancesRegistry AllowancesRegistry { get; init; }

        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageGrantAllowance, Serialization.MessageGrantAllowance>();
            messageRegistry.RegisterMessage<MessageRevokeAllowance, Serialization.MessageRevokeAllowance>();

            _services.AddSingleton(this.AllowancesRegistry);

            this.AllowancesRegistry.Register<Serialization.Allowances.BasicAllowance>(Serialization.Allowances.BasicAllowance.AllowanceType);
            this.AllowancesRegistry.Register<Serialization.Allowances.PeriodicAllowance>(Serialization.Allowances.PeriodicAllowance.AllowanceType);
            this.AllowancesRegistry.Register<Serialization.Allowances.AllowedMessageAllowance>(Serialization.Allowances.AllowedMessageAllowance.AllowanceType);
        }
    }
}
