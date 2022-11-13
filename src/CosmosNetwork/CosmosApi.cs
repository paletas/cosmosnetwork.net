using CosmosNetwork.API;
using CosmosNetwork.Modules.Gov;
using CosmosNetwork.Modules.Staking;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace CosmosNetwork
{
    public class CosmosApi
    {
        public CosmosApi(IServiceProvider serviceProvider)
        {
            this.Blocks = serviceProvider.GetRequiredService<IBlocksApi>();
            this.Transactions = serviceProvider.GetRequiredService<ITransactionsApi>();
            this.Wallet = serviceProvider.GetRequiredService<IWalletApi>();
            this.Staking = serviceProvider.GetRequiredService<IStakingApi>();
            this.Governance = serviceProvider.GetRequiredService<IGovApi>();
        }

        public IBlocksApi Blocks { get; init; }

        public ITransactionsApi Transactions { get; init; }

        public IWalletApi Wallet { get; init; }

        public IStakingApi Staking { get; init; }

        public IGovApi Governance { get; init; }
    }
}
