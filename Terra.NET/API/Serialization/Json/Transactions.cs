using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json
{
    internal record BlockTransaction(long Id, string ChainId, [property: JsonPropertyName("tx")] TransactionPack Details, long Height, [property: JsonPropertyName("txhash")] string Hash, [property: JsonPropertyName("gas_used")] long GasUsed, [property: JsonPropertyName("gas_wanted")] long GasWanted, DateTime Timestamp)
    {
        internal Terra.NET.BlockTransaction ToModel()
        {
            return new Terra.NET.BlockTransaction(
                this.Id,
                this.ChainId,
                this.Details.ToModel(),
                this.Height,
                this.Hash,
                this.GasUsed,
                this.GasWanted,
                this.Timestamp
            );
        }
    };

    internal record MemPoolTransaction([property: JsonPropertyName("chainId")] string ChainId, [property: JsonPropertyName("txhash")] string Hash, DateTime Timestamp, [property: JsonPropertyName("tx")] TransactionPack Details)
    {
        internal Terra.NET.MemPoolTransaction ToModel()
        {
            return new Terra.NET.MemPoolTransaction(
                this.ChainId,
                this.Hash,
                this.Timestamp,
                this.Details.ToModel()
            );
        }
    };

    internal record TransactionPack([property: JsonPropertyName("type")] string TransactionType, StandardTransaction Value)
    {
        internal Terra.NET.Transaction ToModel()
        {
            return new Terra.NET.StandardTransaction(
                this.Value.Messages.Select(msg => msg.ToModel()).ToArray(),
                this.Value.Memo,
                null,
                this.Value.Fees.ToModel(),
                this.Value.Signatures
            );
        }
    };

    internal record StandardTransaction([property: JsonPropertyName("fee")] Fee Fees, [property: JsonPropertyName("msg")] Message[] Messages, string Memo, SignerOptions[] Signatures, ulong? TimeoutHeight);

    internal record Fee(ulong Gas, DenomAmount[] Amount)
    {
        internal Terra.NET.Fee ToModel()
        {
            return new Terra.NET.Fee(this.Gas, this.Amount.Select(amnt => amnt.ToModel()).ToArray());
        }
    };

    internal record AuthInfo([property: JsonPropertyName("signer_infos")] SignerInfo[] Signers, Fee? fee);

    public record TransactionGasUsage(ulong GasWanted, ulong GasUsed);

    public record TransactionSimulationResult(string Data, string Log, TransactionEvent[] Events);

    public record TransactionEvent(string Type, TransactionEventAttribute[] Attributes);

    public record TransactionLog([property: JsonPropertyName("msg_index")] ulong MessageIndex, string Log, TransactionEvent[] Events);

    public record TransactionEventAttribute(string Key, string Value);

    public record TransactionExecutionResult(ulong Height, [property: JsonPropertyName("txhash")] string TransactionHash, string Codespace, ulong Code, string Data, TransactionLog[] Logs, string Info, ulong GasWanted, ulong GasUsed, TransactionEvent[] Events);
}
