using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Responses
{
    internal record DenomMetadataResponse(DenomMetadata[] Metadatas);

    internal record SimulateMarketSwapResponse([property: JsonPropertyName("return_coin")] DenomAmount Result);
}
