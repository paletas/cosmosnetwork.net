namespace Terra.NET.API
{
    public interface IMemPoolApi
    {
        IAsyncEnumerable<MemPoolTransaction> GetPendingTransactions(CancellationToken cancellationToken = default);
    }
}
