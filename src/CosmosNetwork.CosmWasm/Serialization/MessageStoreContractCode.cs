using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    internal record MessageStoreContractCode([property: JsonPropertyName("sender")] string SenderAddress, string WasmByteCode)
        : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new CosmWasm.MessageStoreContractCode(SenderAddress, WasmByteCode);
        }
    }
}
