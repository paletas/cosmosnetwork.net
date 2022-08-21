using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Messages.Serialization
{
    internal record MessageStoreContractCode([property: JsonPropertyName("sender")] string SenderAddress, string WasmByteCode)
        : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Messages.MessageStoreContractCode(SenderAddress, WasmByteCode);
        }
    }
}
