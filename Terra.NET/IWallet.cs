
namespace Terra.NET
{
    public interface IWallet
    {
        string? AccountAdddress { get; }

        PublicKey? PublicKey { get; }

        Task<AccountInformation?> GetAccountInformation(CancellationToken cancellationToken = default);

        Task<AccountBalances> GetBalances(CancellationToken cancellationToken = default);

        Task<Transaction> CreateTransaction(IEnumerable<Message> messages, CreateTransactionOptions transactionOptions, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionSimulation Result)> SimulateTransaction(IEnumerable<Message> messages, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default);

        Task<Fee> EstimateFee(IEnumerable<Message> messages, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default);
    }
}