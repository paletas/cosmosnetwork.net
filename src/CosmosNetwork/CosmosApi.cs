using CosmosNetwork.API;
using CosmosNetwork.API.Impl;
using CosmosNetwork.Modules.Staking;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Terra.NET.Tests")]

namespace CosmosNetwork
{
    public class CosmosApi
    {
        public CosmosApi(IServiceProvider serviceProvider)
        {
            Blocks = serviceProvider.GetRequiredService<IBlocksApi>();
            Transactions = serviceProvider.GetRequiredService<ITransactionsApi>();
            Wallet = serviceProvider.GetRequiredService<IWalletApi>();
            Staking = serviceProvider.GetRequiredService<IStakingApi>();
        }

        public IBlocksApi Blocks { get; init; }

        public ITransactionsApi Transactions { get; init; }

        public IWalletApi Wallet { get; init; }

        public IStakingApi Staking { get; init; }
    }
}
