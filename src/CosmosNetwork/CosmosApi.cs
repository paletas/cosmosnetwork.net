using CosmosNetwork.API;
using CosmosNetwork.API.Impl;
using CosmosNetwork.Modules.Staking;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Terra.NET.Tests")]

namespace CosmosNetwork
{
    public class CosmosApi
    {
        internal CosmosApi(HttpClient httpClient, ILoggerFactory loggerFactory, CosmosApiOptions options)
        {
            Options = options;

            Blocks = new BlocksApi(options, httpClient, loggerFactory.CreateLogger<BlocksApi>());
            Transactions = new TransactionsApi(options, httpClient, loggerFactory.CreateLogger<TransactionsApi>(), Blocks);
            Wallet = new WalletApi(options, httpClient, loggerFactory.CreateLogger<WalletApi>(), Transactions);
            Staking = new StakingApi(options, httpClient, loggerFactory.CreateLogger<StakingApi>());
        }

        public CosmosApiOptions Options { get; init; }

        public IBlocksApi Blocks { get; init; }

        public ITransactionsApi Transactions { get; init; }

        public IWalletApi Wallet { get; init; }

        public IStakingApi Staking { get; init; }
    }
}
