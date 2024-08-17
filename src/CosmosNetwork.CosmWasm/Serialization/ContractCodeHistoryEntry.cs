using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    internal record ContractCodeHistoryEntry(
        ContractCodeHistoryOperationTypes Operation,
        ulong CodeId,
        EntryUpdatedAt Updated,
        [property: JsonPropertyName("msg")] JsonDocument RawMessage)
    {
        public CosmWasm.ContractCodeHistoryEntry ToModel()
        {
            string jsonMessage;
            using (MemoryStream stream = new MemoryStream())
            {
                Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                this.RawMessage.WriteTo(writer);
                writer.Flush();
                jsonMessage = Encoding.UTF8.GetString(stream.ToArray());
            }

            return new CosmWasm.ContractCodeHistoryEntry(
                (CosmWasm.ContractCodeHistoryOperationTypes)this.Operation,
                this.CodeId,
                this.Updated.ToModel(),
                jsonMessage);
        }
    }

    internal record EntryUpdatedAt(ulong BlockHeight, ulong TxIndex)
    {
        public CosmWasm.EntryUpdatedAt ToModel()
        {
            return new CosmWasm.EntryUpdatedAt(this.BlockHeight, this.TxIndex);
        }
    }

    internal enum ContractCodeHistoryOperationTypes
    {
        CONTRACT_CODE_HISTORY_OPERATION_TYPE_UNSPECIFIED = 0,
        CONTRACT_CODE_HISTORY_OPERATION_TYPE_INIT = 1,
        CONTRACT_CODE_HISTORY_OPERATION_TYPE_MIGRATE = 2,
        CONTRACT_CODE_HISTORY_OPERATION_TYPE_GENESIS = 3,
    }
}
