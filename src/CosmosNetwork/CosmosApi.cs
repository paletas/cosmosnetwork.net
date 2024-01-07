using CosmosNetwork.API;
using CosmosNetwork.Modules.Gov;
using CosmosNetwork.Modules.Staking;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork
{
    public class CosmosApi
    {
        public CosmosApi(IServiceProvider serviceProvider)
            : this(CosmosNetworkConfigurator.DEFAULT_KEY, serviceProvider)
        { }

        public CosmosApi([ServiceKey] string serviceKey, IServiceProvider serviceProvider)
        {
            this.Blocks = serviceProvider.GetRequiredKeyedService<IBlocksApi>(serviceKey);
            this.Transactions = serviceProvider.GetRequiredKeyedService<ITransactionsApi>(serviceKey);
            this.Wallet = serviceProvider.GetRequiredKeyedService<IWalletApi>(serviceKey);
            this.Staking = serviceProvider.GetRequiredKeyedService<IStakingApi>(serviceKey);
            this.Governance = serviceProvider.GetRequiredKeyedService<IGovApi>(serviceKey);
        }

        public IBlocksApi Blocks { get; init; }

        public ITransactionsApi Transactions { get; init; }

        public IWalletApi Wallet { get; init; }

        public IStakingApi Staking { get; init; }

        public IGovApi Governance { get; init; }
    }
}
