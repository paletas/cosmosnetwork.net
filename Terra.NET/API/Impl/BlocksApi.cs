using Microsoft.Extensions.Logging;
using Terra.NET.Exceptions;

namespace Terra.NET.API.Impl
{
    internal class BlocksApi : BaseApiSection, IBlocksApi
    {
        public BlocksApi(TerraApiOptions options, HttpClient httpClient, ILogger<BaseApiSection> logger) : base(options, httpClient, logger)
        {
        }

        public async Task<Block?> GetBlock(ulong height, CancellationToken cancellationToken = default)
        {
            var block = await base.Get<Serialization.Json.Block>($"/blocks/{height}", cancellationToken).ConfigureAwait(false);
            if (block == null) return null;

            return block.ToModel();
        }

        public async Task<Block> GetLatestBlock(CancellationToken cancellationToken = default)
        {
            var block = await base.Get<Serialization.Json.Block>($"/blocks/latest", cancellationToken).ConfigureAwait(false);
            if (block == null) throw new TerraApiException("expected a block");

            return block.ToModel();
        }
    }
}
