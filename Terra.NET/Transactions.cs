using System.Runtime.Serialization;

namespace Terra.NET
{
    public record BlockTransaction(long Id, string ChainId, Transaction Details, long Height, string Hash, long GasUsed, long GasWanted, DateTime Timestamp);

    public record MemPoolTransaction(string ChainId, string Hash, DateTime Timestamp, Transaction Details);

    public record Transaction(TransactionTypeEnum TransactionType, string? Memo, ulong? TimeoutHeight, Fee Fees);

    public record StandardTransaction(Message[] Messages, string? Memo, ulong? TimeoutHeight, Fee Fees, SignerOptions[] Signees)
        : Transaction(TransactionTypeEnum.Standard, Memo, TimeoutHeight, Fees);

    public record Fee(ulong GasLimit, Coin[] Amount);

    public record TransactionSimulation(TransactionGasUsage? GasUsage, TransactionSimulationResult? Result);

    public record TransactionBroadcast(SignedTransaction? Transaction, TransactionGasUsage? GasUsage, TransactionBroadcastResult? Result);

    public enum TransactionTypeEnum
    {
        [EnumMember(Value = "core/StdTx")]
        Standard
    }

    public record CreateTransactionOptions(string? Memo = null, ulong? TimeoutHeight = null, Fee? Fees = null, ulong? Gas = null, CoinDecimal[]? GasPrices = null, string[]? FeesDenoms = null);

    public record TransactionSimulationOptions(string? Memo = null, ulong? TimeoutHeight = null, Fee? Fees = null, ulong? Gas = null, CoinDecimal[]? GasPrices = null, string[]? FeesDenoms = null) : CreateTransactionOptions(Memo, TimeoutHeight,  Fees, Gas, GasPrices, FeesDenoms);

    public record BroadcastTransactionOptions(string? Memo = null, ulong? TimeoutHeight = null, Fee? Fees = null, ulong? Gas = null, CoinDecimal[]? GasPrices = null, string[]? FeesDenoms = null) : CreateTransactionOptions(Memo, TimeoutHeight, Fees, Gas, GasPrices, FeesDenoms);

    public record EstimateFeesOptions(string? Memo = null, ulong? TimeoutHeight = null, Fee? Fees = null, ulong? Gas = null, decimal? GasAdjustment = null, CoinDecimal[]? GasPrices = null, string[]? FeesDenoms = null) : TransactionSimulationOptions(Memo, TimeoutHeight, Fees, Gas, GasPrices, FeesDenoms);

    public record TransactionGasUsage(ulong GasWanted, ulong GasUsed);

    public record TransactionSimulationResult(string Data, string Log, TransactionEvent[] Events);

    public record TransactionBroadcastResult(string Data, string Info, TransactionLog[] Logs, TransactionEvent[] Events);

    public record TransactionEvent(string Type, TransactionEventAttribute[] Attributes);

    public record TransactionEventAttribute(string Key, string Value);

    public record TransactionLog(ulong MessageIndex, string Log, TransactionEvent[] Events);

    public record SignedTransaction(byte[] Payload, Fee Fee);
}
