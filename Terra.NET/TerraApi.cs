using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Terra.NET.API;
using Terra.NET.API.Impl;

[assembly: InternalsVisibleTo("Terra.NET.Tests")]

namespace Terra.NET
{
    public class TerraApi
    {
        public TerraApi(HttpClient httpClient, ILoggerFactory loggerFactory, TerraApiOptions? options = default)
        {
            if (options == default)
                options = new TerraApiOptions();

            this.Transactions = new TransactionsApi(options, httpClient, loggerFactory.CreateLogger<TransactionsApi>());
            this.SmartContracts = new SmartContractsApi(options, httpClient, loggerFactory.CreateLogger<SmartContractsApi>());
            this.Blockchain = new BlockchainApi(options, httpClient, loggerFactory.CreateLogger<BlockchainApi>());
            this.Wallet = new WalletApi(options, httpClient, loggerFactory.CreateLogger<WalletApi>(), this.Transactions);
        }

        public TerraApiOptions Options { get; init; }

        public ITransactionsApi Transactions { get; init; }

        public ISmartContractsApi SmartContracts { get; init; }

        public IBlockchainApi Blockchain { get; init; }

        public IWalletApi Wallet { get; init; }
    }
}
