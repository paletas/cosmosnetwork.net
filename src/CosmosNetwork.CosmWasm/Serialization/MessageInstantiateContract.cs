using CosmosNetwork.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    internal record MessageInstantiateContract(
        [property: JsonPropertyName("sender")] string SenderAddress,
        [property: JsonPropertyName("admin")] string? AdminAddress,
        ulong CodeId,
        string Label,
        JsonDocument Msg,
        DenomAmount[] Funds) : SerializerMessage
    {
        protected override Message ToModel()
        {
            string initMessageJson = JsonSerializer.Serialize(Msg);

            return new CosmWasm.MessageInstantiateContract(
                SenderAddress,
                string.IsNullOrWhiteSpace(AdminAddress) ? null : new CosmosAddress(AdminAddress),
                CodeId,
                Label,
                initMessageJson,
                Funds.Select(coin => coin.ToModel()).ToArray());
        }
    }
}
