﻿using CosmosNetwork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

ServiceCollection services = new ServiceCollection();
services.AddCosmosNetwork("cosmoshub-4", "https://api.cosmos.network/", new CosmosApiOptions())
    .SetupCosmosHub();

services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Information).AddConsole());

var serviceProvider = services.BuildServiceProvider();
serviceProvider.UseCosmosNetwork();

var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
var cosmosApi = serviceProvider.GetRequiredService<CosmosApi>();

var latestBlock = await cosmosApi.Blocks.GetLatestBlock();

do
{
    ulong latestHeight = latestBlock.Details.Header.Height;
    ulong currentHeight = await GetCurrentHeight() ?? 5213849;

    try
    {
        while (currentHeight < latestHeight)
        {
            logger.LogInformation($"Searching block {currentHeight}..");
            await foreach (var tx in cosmosApi.Transactions.GetTransactions(currentHeight))
            {
                logger.LogInformation($"  Block {currentHeight} found tx: {tx.Hash}");

                foreach (var msg in tx.Details.Messages)
                {
                    logger.LogInformation($"     Message {msg.GetType().Name}");
                }
            }

            currentHeight++;
            await SaveCurrentHeight(currentHeight);
        }
    }
    catch (TimeoutException)
    {
        logger.LogError("[ERROR] TimeOut!");
    }
    catch (Exception ex)
    {
        logger.LogError($"[ERROR] {ex}");
    }
}
while (true);

async Task<ulong?> GetCurrentHeight()
{
    if (File.Exists("local.state") == false) return null;
    return ulong.Parse(await File.ReadAllTextAsync("local.state"));
}

async Task SaveCurrentHeight(ulong height)
{
    await File.WriteAllTextAsync("local.state", height.ToString());
}