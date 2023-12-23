using Microsoft.Extensions.Logging;

namespace CosmosNetwork.Tests.Integration.Tests
{
    internal class CrawlerTest : IIntegrationTest
    {
        private readonly ILogger<CrawlerTest> _logger;
        private readonly CosmosApi _cosmosApi;

        public CrawlerTest(ILogger<CrawlerTest> logger, CosmosApi cosmosApi)
        {
            _logger = logger;
            _cosmosApi = cosmosApi;
        }

        public string Name { get { return "Crawler - Message Deserialization"; } }

        public async Task Execute(IWallet wallet, CancellationToken cancellationToken = default)
        {
            var latestBlock = await this._cosmosApi.Blocks.GetLatestBlock(cancellationToken);

            do
            {
                ulong latestHeight = latestBlock.Details.Header.Height;
                ulong currentHeight = await GetCurrentHeight() ?? 1;

                try
                {
                    while (currentHeight < latestHeight)
                    {
                        _logger.LogInformation($"Searching block {currentHeight}..");
                        await foreach (var tx in _cosmosApi.Transactions.GetTransactions(currentHeight, cancellationToken))
                        {
                            _logger.LogInformation($"  Block {currentHeight} found tx: {tx.Hash}");

                            foreach (var msg in tx.Details.Messages)
                            {
                                _logger.LogInformation($"     Message {msg.GetType().Name}");
                            }
                        }

                        currentHeight++;
                        await SaveCurrentHeight(currentHeight);
                    }
                }
                catch (TimeoutException)
                {
                    _logger.LogError("[ERROR] TimeOut!");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"[ERROR] {ex}");
                }
            }
            while (true);
        }

        private static async Task<ulong?> GetCurrentHeight()
        {
            if (File.Exists("local.state") == false) return default;
            return ulong.Parse(await File.ReadAllTextAsync("local.state"));
        }

        private static async Task SaveCurrentHeight(ulong height)
        {
            await File.WriteAllTextAsync("local.state", height.ToString());
        }
    }
}
