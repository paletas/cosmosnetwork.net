namespace Terra.NET.SmartContracts.Shared
{
    public record Expiration();

    public record AtHeightExpiration(ulong Height) : Expiration();

    public record AtTimeExpiration(DateTime Time) : Expiration();

    public record NeverExpiration() : Expiration();
}
