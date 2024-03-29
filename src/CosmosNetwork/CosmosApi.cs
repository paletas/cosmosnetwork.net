using CosmosNetwork.API;
using CosmosNetwork.Modules.Gov;
using CosmosNetwork.Modules.Staking;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork
{
    public partial class CosmosApi([ServiceKey] string serviceKey, IServiceProvider serviceProvider)
    {
        public CosmosApi(IServiceProvider serviceProvider)
            : this(CosmosNetworkConfigurator.DEFAULT_KEY, serviceProvider)
        { }

        public IBlocksApi Blocks { get; init; } = serviceProvider.GetRequiredKeyedService<IBlocksApi>(serviceKey);

        public ITransactionsApi Transactions { get; init; } = serviceProvider.GetRequiredKeyedService<ITransactionsApi>(serviceKey);

        public IWalletApi Wallet { get; init; } = serviceProvider.GetRequiredKeyedService<IWalletApi>(serviceKey);

        public IStakingApi Staking { get; init; } = serviceProvider.GetRequiredKeyedService<IStakingApi>(serviceKey);

        public IGovApi Governance { get; init; } = serviceProvider.GetRequiredKeyedService<IGovApi>(serviceKey);
    }
}
