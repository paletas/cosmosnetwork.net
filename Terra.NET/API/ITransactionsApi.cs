namespace Terra.NET.API
{
    public interface ITransactionsApi
    {
        IAsyncEnumerable<BlockTransaction> GetAccountTransactions(TerraAddress accountAddress, CancellationToken cancellationToken = default);

        IAsyncEnumerable<BlockTransaction> GetTransactions(long? fromHeight = null, CancellationToken cancellationToken = default);

        Task<Transaction> CreateTransaction(IEnumerable<Message> messages, SignerOptions[] signers, CreateTransactionOptions transactionOptions, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionSimulation? Result)> SimulateTransaction(IEnumerable<Message> messages, SignerOptions[] signers, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default);

        Task<Fee> EstimateFee(IEnumerable<Message> messages, SignerOptions[] signers, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default);
    }
}