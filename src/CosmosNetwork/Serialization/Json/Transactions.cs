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
                    this.Transaction.Body.Messages.Select(msg => msg.ToModel()).ToArray(),
                    this.Transaction.Body.Memo,
                    this.Transaction.Body.TimeoutHeight,
                    this.Transaction.AuthInfo.Fee.ToModel(),
                    this.Transaction.AuthInfo.Signers.Select((sig, ix) =>
                        new CosmosNetwork.TransactionSigner(
                            sig.PublicKey.ToModel(),
                            new CosmosNetwork.SignatureDescriptor(SignerModeEnum.Direct, this.Transaction.Signatures[ix]),
                            sig.Sequence
                        )
                    ).ToArray()
                ),
                this.Response.Height,
                this.Response.TransactionHash,
                this.Response.GasUsed,
                this.Response.GasWanted,
                this.Response.Timestamp,
                this.Response.RawLog,
                this.Response.Logs.Select(log => log.ToModel()).ToArray(),
                this.Response.GetExecutionStatus()
            );
        }
    }

    internal record Transaction(TransactionBody Body, AuthInfo AuthInfo, string[] Signatures);

    internal record TransactionBody(SerializerMessage[] Messages, string Memo, ulong? TimeoutHeight);

    internal record Fee(ulong Gas, DenomAmount[] Amount)
    {
        internal CosmosNetwork.Fee ToModel()
        {
            return new CosmosNetwork.Fee(this.Gas, this.Amount.Select(amnt => amnt.ToModel()).ToArray());
        }
    }

    internal record AuthInfo([property: JsonPropertyName("signer_infos")] SignatureDescriptor[] Signers, Fee Fee);

    public record TransactionGasUsage(ulong GasWanted, ulong GasUsed);

    public record TransactionSimulationResult(string Data, string Log, TransactionEvent[] Events);

    public record TransactionEventAttribute(string Key, string Value)
    {
        public CosmosNetwork.TransactionEventAttribute ToModel()
        {
            return new CosmosNetwork.TransactionEventAttribute(this.Key, this.Value);
        }
    }

    public record TransactionEvent(string Type, TransactionEventAttribute[] Attributes)
    {
        public CosmosNetwork.TransactionEvent ToModel()
        {
            return new CosmosNetwork.TransactionEvent(this.Type, this.Attributes.Select(attr => attr.ToModel()).ToArray());
        }
    }

    public record TransactionLog([property: JsonPropertyName("msg_index")] ulong MessageIndex, JsonDocument Log, TransactionEvent[] Events)
    {
        public CosmosNetwork.TransactionLog ToModel()
        {
            return new CosmosNetwork.TransactionLog(this.MessageIndex, this.Log?.ToString(), this.Events.Select(evt => evt.ToModel()).ToArray());
        }
    }

    public record TransactionResponse(
        ulong Height, 
        [property: JsonPropertyName("txhash")] string TransactionHash, 
        string Codespace, 
        ulong Code,
        string Data, 
        string RawLog, 
        TransactionLog[] Logs, 
        string Info, 
        ulong GasWanted, ulong GasUsed, 
        TransactionEvent[] Events, 
        [property: JsonPropertyName("timestamp")] DateTime Timestamp)
    {
        internal ExecutionResult ToModel()
        {
            return new ExecutionResult(this.Data, this.Info, this.Logs.Select(log => log.ToModel()).ToArray(), this.Events.Select(evt => evt.ToModel()).ToArray());
        }

        internal TransactionExecutionStatus GetExecutionStatus()
        {
            return this.Code switch
            {
                0 => TransactionExecutionStatus.Executed,
                _ => TransactionExecutionStatus.Failed,
            };
        }
    }
}
