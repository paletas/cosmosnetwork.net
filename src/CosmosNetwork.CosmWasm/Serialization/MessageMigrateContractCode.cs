using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    internal record MessageMigrateContractCode(
        [property: JsonPropertyName("sender")] string SenderAddress,
        ulong CodeId,
        string WasmByteCode) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new CosmWasm.MessageMigrateContractCode(SenderAddress, CodeId, WasmByteCode);
        }
    }
}
