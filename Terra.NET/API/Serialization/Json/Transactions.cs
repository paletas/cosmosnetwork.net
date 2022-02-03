using System.Text.Json.Serialization;
using Terra.NET.API.Serialization.Json.Converters;

namespace Terra.NET.API.Serialization.Json
{
    internal record BlockTransaction([property: JsonPropertyName("tx")] Transaction Transaction, [property: JsonPropertyName("tx_response")] TransactionResponse Response)
    {
        internal Terra.NET.BlockTransaction ToModel()
        {
            return new Terra.NET.BlockTransaction(
                this.Transaction.ToModel(),
                this.Response.Height,
                this.Response.TransactionHash,
                this.Response.GasUsed,
                this.Response.GasWanted,
                this.Response.Timestamp,
                this.Response.RawLog,
                this.Response.Logs.Select(log => log.ToModel()).ToArray()
            );
        }
    };

    internal record Transaction(TransactionBody Body, AuthInfo AuthInfo, string[] Signatures)
    {
        internal Terra.NET.Transaction ToModel()
        {
            return new Terra.NET.Transaction(Terra.NET.TransactionTypeEnum.Standard, this.Body.Memo, this.Body.TimeoutHeight, this.AuthInfo.Fee.ToModel());
        }
    };

    internal record TransactionBody([property: JsonConverter(typeof(MessagesConverter))] Message[] Messages, string Memo, ulong? TimeoutHeight);

    internal record MemPoolTransaction([property: JsonPropertyName("chainId")] string ChainId, [property: JsonPropertyName("txhash")] string Hash, DateTime Timestamp, [property: JsonPropertyName("tx")] TransactionPack Details)
    {
        internal Terra.NET.MemPoolTransaction ToModel()
        {
            return new Terra.NET.MemPoolTransaction(
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

    internal record StandardTransaction([property: JsonPropertyName("fee")] Fee Fees, [property: JsonPropertyName("msg"), JsonConverter(typeof(MessagesConverter))] Message[] Messages, string Memo, SignerOptions[] Signatures, ulong? TimeoutHeight);

    internal record Fee(ulong Gas, DenomAmount[] Amount)
    {
        internal Terra.NET.Fee ToModel()
        {
            return new Terra.NET.Fee(this.Gas, this.Amount.Select(amnt => amnt.ToModel()).ToArray());
        }
    };

    internal record AuthInfo([property: JsonPropertyName("signer_infos")] SignerInfo[] Signers, Fee? Fee);

    public record TransactionGasUsage(ulong GasWanted, ulong GasUsed);

    public record TransactionSimulationResult(string Data, string Log, TransactionEvent[] Events);

    public record TransactionEventAttribute(string Key, string Value)
    {
        public Terra.NET.TransactionEventAttribute ToModel()
        {
            return new Terra.NET.TransactionEventAttribute(this.Key, this.Value);
        }
    };

    public record TransactionEvent(string Type, TransactionEventAttribute[] Attributes)
    {
        public Terra.NET.TransactionEvent ToModel()
        {
            return new NET.TransactionEvent(this.Type, this.Attributes.Select(attr => attr.ToModel()).ToArray());
        }
    };

    public record TransactionLog([property: JsonPropertyName("msg_index")] ulong MessageIndex, string Log, TransactionEvent[] Events)
    {
        public Terra.NET.TransactionLog ToModel()
        {
            return new NET.TransactionLog(this.MessageIndex, this.Log, this.Events.Select(evt => evt.ToModel()).ToArray());
        }
    };

    public record TransactionResponse(ulong Height, [property: JsonPropertyName("txhash")] string TransactionHash, string Codespace, ulong Code, string Data, string RawLog, TransactionLog[] Logs, string Info, ulong GasWanted, ulong GasUsed, TransactionEvent[] Events, [property: JsonPropertyName("tx/timestamp")] DateTime Timestamp)
    {
        internal Terra.NET.TransactionResult ToModel()
        {
            return new TransactionResult(this.Data, this.Info, this.Logs.Select(log => log.ToModel()).ToArray(), this.Events.Select(evt => evt.ToModel()).ToArray());
        }
    };
}
