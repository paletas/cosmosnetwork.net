using CosmosNetwork.Modules.FeeGrant.Allowances;
using CosmosNetwork.Modules.FeeGrant.Serialization.Allowances;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.FeeGrant
{
    internal static class FeeGrantConfiguration
    {
        public static IServiceCollection ConfigureModule(this IServiceCollection serviceCollection, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageGrantAllowance>();
            messageRegistry.RegisterMessage<MessageRevokeAllowance>();

            AllowancesRegistry allowancesRegistry = new();
            _ = serviceCollection.AddSingleton(allowancesRegistry);

            allowancesRegistry.Register<Serialization.Allowances.BasicAllowance>(Serialization.Allowances.BasicAllowance.AllowanceType);
            allowancesRegistry.Register<Serialization.Allowances.PeriodicAllowance>(Serialization.Allowances.PeriodicAllowance.AllowanceType);
            allowancesRegistry.Register<Serialization.Allowances.AllowedMessageAllowance>(Serialization.Allowances.AllowedMessageAllowance.AllowanceType);

            return serviceCollection;
        }
    }
}
