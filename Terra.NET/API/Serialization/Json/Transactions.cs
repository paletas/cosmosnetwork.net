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

    internal record StandardTransaction([property: JsonPropertyName("fee")] Fee Fees, [property: JsonPropertyName("msg")] Message[] Messages, string Memo, SignerOptions[] Signatures);

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

    public record TransactionEventAttribute(string Key, string Value);
}
