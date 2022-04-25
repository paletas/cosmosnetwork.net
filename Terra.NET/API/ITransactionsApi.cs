namespace Terra.NET.API
{
    public interface ITransactionsApi
    {
        IAsyncEnumerable<BlockTransaction> GetTransactions(TerraAddress accountAddress, CancellationToken cancellationToken = default);

        IAsyncEnumerable<BlockTransaction> GetTransactions(ulong fromHeight, CancellationToken cancellationToken = default);

        Task<BlockTransaction?> GetTransaction(string transactionHash, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(IEnumerable<Message> messages, SignerOptions[] signers, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(SignedTransaction transaction, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionBlock(SignedTransaction transaction, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionBroadcast? Result)> BroadcastTransactionAsyncAndWait(SignedTransaction transaction, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, string? TransactionHash)> BroadcastTransactionAsync(SignedTransaction transaction, CancellationToken cancellationToken = default);

        Task<Fee> EstimateFee(IEnumerable<Message> messages, SignerOptions[] signers, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default);

        Task<IEnumerable<Coin>> ComputeTax(IEnumerable<Message> messages, SignerOptions[] signers, CancellationToken cancellationToken = default);
    }
}
