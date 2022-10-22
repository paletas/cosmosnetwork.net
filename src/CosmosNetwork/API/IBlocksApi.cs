namespace CosmosNetwork.API
{
    public interface IBlocksApi
    {
        Task<Block?> GetBlock(ulong height, CancellationToken cancellationToken = default);

        Task<Block> GetLatestBlock(CancellationToken cancellationToken = default);
    }
}
