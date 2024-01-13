using CosmosNetwork.Modules.Bank.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Responses
{
    internal record DenomMetadataResponse(DenomMetadata[] Metadatas);

    internal record SimulateMarketSwapResponse([property: JsonPropertyName("return_coin")] DenomAmount Result);
}
