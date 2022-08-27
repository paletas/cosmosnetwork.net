using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json
{
    internal record BlockTransaction([property: JsonPropertyName("tx")] Transaction Transaction, [property: JsonPropertyName("tx_response")] TransactionResponse Response)
    {
        internal CosmosNetwork.BlockTransaction ToModel()
        {
            return new CosmosNetwork.BlockTransaction(
                new CosmosNetwork.Transaction(
                    Transaction.Body.Messages.Select(msg => msg.ToModel()).ToArray(),
                    Transaction.Body.Memo,
                    Transaction.Body.TimeoutHeight,
                    Transaction.AuthInfo.Fee.ToModel(),
                    Transaction.AuthInfo.Signers.Select((sig, ix) => new CosmosNetwork.TransactionSignature(sig.PublicKey.ToModel(), Transaction.Signatures[ix])).ToArray()
                ),
                Response.Height,
                Response.TransactionHash,
                Response.GasUsed,
                Response.GasWanted,
                Response.Timestamp,
                Response.RawLog,
                Response.Logs.Select(log => log.ToModel()).ToArray(),
                Response.GetExecutionStatus()
            );
        }
    }

    internal record Transaction(TransactionBody Body, AuthInfo AuthInfo, string[] Signatures);

    internal record TransactionBody(SerializerMessage[] Messages, string Memo, ulong? TimeoutHeight);

    internal record Fee(ulong Gas, DenomAmount[] Amount)
    {
        internal CosmosNetwork.Fee ToModel()
        {
            return new CosmosNetwork.Fee(Gas, Amount.Select(amnt => amnt.ToModel()).ToArray());
        }
    }

    internal record AuthInfo([property: JsonPropertyName("signer_infos")] SignatureDescriptor[] Signers, Fee Fee);

    public record TransactionGasUsage(ulong GasWanted, ulong GasUsed);

    public record TransactionSimulationResult(string Data, string Log, TransactionEvent[] Events);

    public record TransactionEventAttribute(string Key, string Value)
    {
        public CosmosNetwork.TransactionEventAttribute ToModel()
        {
            return new CosmosNetwork.TransactionEventAttribute(Key, Value);
        }
    }

    public record TransactionEvent(string Type, TransactionEventAttribute[] Attributes)
    {
        public CosmosNetwork.TransactionEvent ToModel()
        {
            return new CosmosNetwork.TransactionEvent(Type, Attributes.Select(attr => attr.ToModel()).ToArray());
        }
    }

    public record TransactionLog([property: JsonPropertyName("msg_index")] ulong MessageIndex, JsonDocument Log, TransactionEvent[] Events)
    {
        public CosmosNetwork.TransactionLog ToModel()
        {
            return new CosmosNetwork.TransactionLog(MessageIndex, Log?.ToString(), Events.Select(evt => evt.ToModel()).ToArray());
        }
    }

    public record TransactionResponse(ulong Height, [property: JsonPropertyName("txhash")] string TransactionHash, string Codespace, ulong Code, string Data, string RawLog, TransactionLog[] Logs, string Info, ulong GasWanted, ulong GasUsed, TransactionEvent[] Events, [property: JsonPropertyName("tx/timestamp")] DateTime Timestamp)
    {
        internal TransactionResult ToModel()
        {
            return new TransactionResult(Data, Info, Logs.Select(log => log.ToModel()).ToArray(), Events.Select(evt => evt.ToModel()).ToArray());
        }

        internal TransactionExecutionStatus GetExecutionStatus()
        {
            return Code switch
            {
                0 => TransactionExecutionStatus.Executed,
                _ => TransactionExecutionStatus.Failed,
            };
        }
    }
}
