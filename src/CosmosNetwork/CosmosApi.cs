using CosmosNetwork.API;
using CosmosNetwork.API.Impl;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Terra.NET.Tests")]

namespace CosmosNetwork
{
    public class CosmosApi
    {
        public CosmosApi(HttpClient httpClient, ILoggerFactory loggerFactory, CosmosApiOptions? options = default)
        {
            if (options == default)
                options = new CosmosApiOptions();

            Options = options;

            SmartContracts = new SmartContractsApi(options, httpClient, loggerFactory.CreateLogger<SmartContractsApi>());
            Blockchain = new BlockchainApi(options, httpClient, loggerFactory.CreateLogger<BlockchainApi>());
            Blocks = new BlocksApi(options, httpClient, loggerFactory.CreateLogger<BlocksApi>());
            Transactions = new TransactionsApi(options, httpClient, loggerFactory.CreateLogger<TransactionsApi>(), Blockchain, Blocks);
            Wallet = new WalletApi(options, httpClient, loggerFactory.CreateLogger<WalletApi>(), Transactions);
        }

        public CosmosApiOptions Options { get; init; }

        public IBlocksApi Blocks { get; init; }

        public ITransactionsApi Transactions { get; init; }

        public ISmartContractsApi SmartContracts { get; init; }

        public IBlockchainApi Blockchain { get; init; }

        public IWalletApi Wallet { get; init; }
    }
}
