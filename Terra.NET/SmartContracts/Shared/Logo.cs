namespace Terra.NET.SmartContracts.Shared
{
    public record Logo(UrlLogo? Url, EmbeddedLogo? Embedded);

    public record UrlLogo(string Url);

    public record EmbeddedLogo(SvgLogo? Svg, PngLogo? Png);

    public record SvgLogo(byte[] Image);

    public record PngLogo(byte[] Image);
}
