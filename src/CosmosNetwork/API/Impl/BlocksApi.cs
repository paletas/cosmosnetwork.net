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
            var block = await Get<Serialization.Json.Block>($"/cosmos/base/tendermint/v1beta1/blocks/{height}", cancellationToken).ConfigureAwait(false);
            if (block == null) return null;

            return block.ToModel();
        }

        public async Task<Block> GetLatestBlock(CancellationToken cancellationToken = default)
        {
            var block = await Get<Serialization.Json.Block>($"/cosmos/base/tendermint/v1beta1/blocks/latest", cancellationToken).ConfigureAwait(false);
            if (block == null) throw new CosmosException("expected a block");

            return block.ToModel();
        }
    }
}
