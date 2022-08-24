﻿using CosmosNetwork.Exceptions;
using Microsoft.Extensions.Logging;

namespace CosmosNetwork.API.Impl
{
    internal class BlocksApi : BaseApiSection, IBlocksApi
    {
        public BlocksApi(CosmosApiOptions options, HttpClient httpClient, ILogger<BaseApiSection> logger) : base(options, httpClient, logger)
        {
        }

        public async Task<Block?> GetBlock(ulong height, CancellationToken cancellationToken = default)
        {
            Serialization.Json.Block? block = await Get<Serialization.Json.Block>($"/cosmos/base/tendermint/v1beta1/blocks/{height}", cancellationToken).ConfigureAwait(false);
            return block == null ? null : block.ToModel();
        }

        public async Task<Block> GetLatestBlock(CancellationToken cancellationToken = default)
        {
            Serialization.Json.Block? block = await Get<Serialization.Json.Block>($"/cosmos/base/tendermint/v1beta1/blocks/latest", cancellationToken).ConfigureAwait(false);
            return block == null ? throw new CosmosException("expected a block") : block.ToModel();
        }
    }
}