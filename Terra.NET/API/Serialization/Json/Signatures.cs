namespace Terra.NET.API.Serialization.Json
{
    internal record SignerInfo(string PublicKey, SignerModeInfo ModeInfo);

    internal record SignerModeInfo(SignerSingleMode Single);

    internal record SignerSingleMode(SignerModeEnum Mode);
}
