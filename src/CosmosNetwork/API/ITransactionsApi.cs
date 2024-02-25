namespace CosmosNetwork.API
{
    public interface ITransactionsApi
    {
        IAsyncEnumerable<BlockTransaction> GetTransactions(ulong fromHeight, CancellationToken cancellationToken = default);

        Task<BlockTransaction?> GetTransaction(string transactionHash, CancellationToken cancellationToken = default);

        Task<BlockTransaction?> GetTransactionByRawHash(string transactionHash, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionSimulationResults? Result)> SimulateTransaction(ITransactionPayload transaction, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransaction(ITransactionPayload transaction, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionAsync(ITransactionPayload transaction, CancellationToken cancellationToken = default);

        Task<Fee> EstimateFee(ITransactionPayload transaction, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default);
    }
}
