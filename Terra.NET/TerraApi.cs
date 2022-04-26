using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Terra.NET.API;
using Terra.NET.API.Impl;
using Terra.NET.Transactions;

[assembly: InternalsVisibleTo("Terra.NET.Tests")]

namespace Terra.NET
{
    public class TerraApi
    {
        private readonly TransactionsBuilder _transactionsBuilder;

        public TerraApi(HttpClient httpClient, ILoggerFactory loggerFactory, TerraApiOptions? options = default)
        {
            if (options == default)
                options = new TerraApiOptions();

            this._transactionsBuilder = new TransactionsBuilder(options, loggerFactory.CreateLogger<TransactionsBuilder>());

            this.Options = options;

            this.SmartContracts = new SmartContractsApi(options, httpClient, loggerFactory.CreateLogger<SmartContractsApi>());
            this.Blockchain = new BlockchainApi(options, httpClient, loggerFactory.CreateLogger<BlockchainApi>());
            this.Blocks = new BlocksApi(options, httpClient, loggerFactory.CreateLogger<BlocksApi>());
            this.Transactions = new TransactionsApi(options, httpClient, loggerFactory.CreateLogger<TransactionsApi>(), this._transactionsBuilder, this.Blockchain, this.Blocks);
            this.Wallet = new WalletApi(options, httpClient, loggerFactory.CreateLogger<WalletApi>(), this.Transactions, this._transactionsBuilder);
        }

        public TerraApiOptions Options { get; init; }

        public IBlocksApi Blocks { get; init; }

        public ITransactionsApi Transactions { get; init; }

        public ISmartContractsApi SmartContracts { get; init; }

        public IBlockchainApi Blockchain { get; init; }

        public IWalletApi Wallet { get; init; }
    }
}
