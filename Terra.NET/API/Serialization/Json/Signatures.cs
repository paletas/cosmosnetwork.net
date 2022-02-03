using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json
{
    internal record SignerInfo(PublicKey PublicKey, SignerModeInfo ModeInfo);

    internal record SignerModeInfo(SignerSingleMode Single);

    internal record SignerSingleMode(SignerModeEnum Mode);

    internal record PublicKey([property: JsonPropertyName("@type")] string Type, string Key);
}
