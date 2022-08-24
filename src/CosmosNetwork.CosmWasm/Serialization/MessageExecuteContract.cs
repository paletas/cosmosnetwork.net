using CosmosNetwork.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    internal record MessageExecuteContract(
        [property: JsonPropertyName("funds")] DenomAmount[] Coins,
        [property: JsonPropertyName("sender")] string SenderAddress,
        [property: JsonPropertyName("contract")] string ContractAddress,
        [property: JsonPropertyName("msg")] JsonDocument ExecuteMessage) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new CosmWasm.MessageExecuteContract(
                Coins.Select(c => c.ToModel()).ToArray(),
                SenderAddress,
                ContractAddress,
                ToJsonString(ExecuteMessage));
        }

        private static string ToJsonString(JsonDocument jdoc)
        {
            using MemoryStream stream = new();
            Utf8JsonWriter writer = new(stream, new JsonWriterOptions { Indented = true });
            jdoc.WriteTo(writer);
            writer.Flush();
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
