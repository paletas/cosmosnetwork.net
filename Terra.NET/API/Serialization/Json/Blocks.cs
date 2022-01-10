using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json
{
    internal record Block(BlockHeader Header);

    internal record BlockHeader([property: JsonPropertyName("chain-id")] string ChainId, long Height, DateTime Time);
}
