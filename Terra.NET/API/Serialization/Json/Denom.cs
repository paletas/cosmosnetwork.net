using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json
{
    internal record DenomMetadata(string Description, [property: JsonPropertyName("denom_units")] DenomUnit[] Units, [property: JsonPropertyName("base")] string BaseDenom, [property: JsonPropertyName("display")] string DisplayDenom, string Name, string Symbol);

    internal record DenomUnit(string Denom, [property: JsonPropertyName("exponent")] ushort Decimals, string[] Aliases);
}
