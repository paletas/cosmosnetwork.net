using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork
{
    public record BlockTransaction(Transaction Details, ulong Height, string Hash, ulong GasUsed, ulong GasWanted, DateTime Timestamp, string RawLog, TransactionLog[] Logs, TransactionExecutionStatus Status);

    public record Transaction(IEnumerable<Message> Messages, string? Memo, ulong? TimeoutHeight, Fee? Fees = null, TransactionSigner[]? Signees = null)
    {
        internal Serialization.Proto.Transaction ToSerialization()
        {
            return new Serialization.Proto.Transaction(
                new Serialization.Proto.TransactionBody(this.Messages.Select(msg => Any.Pack(msg.ToSerialization())).ToArray(), this.TimeoutHeight, this.Memo),
                new Serialization.Proto.AuthInfo(
                    this.Signees?.Select(sig => new Serialization.SignatureDescriptor
                    {
                        PublicKey = sig.Key.ToProto(),
                        Data = new Serialization.SignatureMode
                        {
                            Single = new Serialization.SingleMode(Serialization.SignModeEnum.Direct)
                        },
                        Sequence = sig.Sequence
                    }).ToList() ?? new List<Serialization.SignatureDescriptor>(),
                    new Serialization.Fee(
                        this.Fees?.Amount?.Select(amt => amt.ToSerialization()).ToArray() ?? Array.Empty<Serialization.DenomAmount>(),
                        this.Fees?.GasLimit,
                        null,
                        null
                    )
                ),
                new List<byte[]>()
            );
        }

        internal SignDoc ToSignatureDocument(string chainId, ulong accountNumber)
        {
            return new Serialization.Proto.SignDoc(
                new Serialization.Proto.TransactionBody(this.Messages.Select(msg => Any.Pack(msg.ToSerialization())).ToArray(), this.TimeoutHeight, this.Memo),
                new Serialization.Proto.AuthInfo(new List<Serialization.SignatureDescriptor>(), this.Fees?.ToSerialization() ?? new Serialization.Fee()),
                chainId, accountNumber);
        }
    }

    public record SerializedTransaction(byte[] Payload, Fee? Fees) : ITransactionPayload
    {
        public string GetBase64()
        {
            return ToSerialization().GetBase64();
        }

        public byte[] GetBytes()
        {
            return ToSerialization().GetBytes();
        }

        internal Serialization.SerializedTransaction ToSerialization()
        {
            return new Serialization.SerializedTransaction(this.Payload, this.Fees?.ToSerialization());
        }
    }

    public record Fee(ulong? GasLimit, Coin[]? Amount, CosmosAddress? Payer = null, CosmosAddress? Granter = null)
    {
        internal Serialization.Fee ToSerialization()
        {
            return new Serialization.Fee(this.Amount?.Select(c => c.ToSerialization()).ToArray(), this.GasLimit, this.Payer?.Address, this.Granter?.Address);
        }
    }

    public record TransactionSimulationResults(GasUsage GasUsage, SimulationResult Result);

    public record TransactionBroadcastResults(GasUsage GasUsage, ExecutionResult Result);

    public record GasUsage(ulong GasWanted, ulong GasUsed);

    public record SimulationResult(string Data, string Log, TransactionEvent[] Events);

    public record ExecutionResult(string Data, string Info, TransactionLog[] Logs, TransactionEvent[] Events);

    public record TransactionEvent(string Type, TransactionEventAttribute[] Attributes);

    public record TransactionEventAttribute(string Key, string Value);

    public record TransactionLog(ulong MessageIndex, string Log, TransactionEvent[] Events);

    public record CreateTransactionOptions(Fee Fees, string? Memo = null, ulong? TimeoutHeight = null, ulong? Gas = null, CoinDecimal[]? GasPrices = null, string[]? FeesDenoms = null);

    public record BroadcastTransactionOptions(Fee Fees, string? Memo = null, ulong? TimeoutHeight = null, ulong? Gas = null, CoinDecimal[]? GasPrices = null, string[]? FeesDenoms = null)
        : CreateTransactionOptions(Fees, Memo, TimeoutHeight, Gas, GasPrices, FeesDenoms);

    public record TransactionSimulationOptions(Fee? Fees = null, string? Memo = null, ulong? TimeoutHeight = null, ulong? Gas = null, CoinDecimal[]? GasPrices = null, string[]? FeesDenoms = null);

    public record EstimateFeesOptions(string? Memo = null, ulong? TimeoutHeight = null, ulong? Gas = null, decimal? GasAdjustment = null, CoinDecimal[]? GasPrices = null, string[]? FeesDenoms = null)
        : TransactionSimulationOptions(null, Memo, TimeoutHeight, Gas, GasPrices, FeesDenoms);

    public enum TransactionExecutionStatus
    {
        MemPool = 1,
        Executed = 2,
        Failed = 3
    }
}
