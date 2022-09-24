using CosmosNetwork.Keys;

namespace CosmosNetwork
{
    public interface IWallet
    {
        CosmosAddress? AccountAddress { get; }

        string? AccountNumber { get; }

        ulong? Sequence { get; }

        IEnumerable<IKey> GetKeys();

        Task UpdateAccountInformation(CancellationToken cancellationToken = default);

        Task<AccountInformation?> GetAccountInformation(CancellationToken cancellationToken = default);

        Task<AccountBalances> GetBalances(CancellationToken cancellationToken = default);

        Task<SerializedTransaction> CreateSignedTransaction(IEnumerable<Message> messages, CreateTransactionOptions transactionOptions, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionSimulationResults? Result)> SimulateTransaction(IEnumerable<Message> messages, TransactionSimulationOptions? simulationOptions = null, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionSimulationResults? Result)> SimulateTransaction(SerializedTransaction transaction, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionBlock(IEnumerable<Message> messages, BroadcastTransactionOptions? broadcastOptions = null, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionBlock(SerializedTransaction transaction, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionAsyncAndWait(IEnumerable<Message> messages, BroadcastTransactionOptions? broadcastOptions = null, CancellationToken cancellationToken = default);

        Task<(uint? ErrorCode, TransactionBroadcastResults? Result)> BroadcastTransactionAsyncAndWait(SerializedTransaction transaction, CancellationToken cancellationToken = default);

        Task<Fee> EstimateFee(IEnumerable<Message> messages, EstimateFeesOptions? estimateOptions = null, CancellationToken cancellationToken = default);
    }
}
