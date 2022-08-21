using CosmosNetwork.Serialization;

namespace CosmosNetwork
{
    public record DenomMetadata(string Description, DenomUnit[] Units, string BaseDenom, string DisplayDenom, string Name, string Symbol);

    public record DenomUnit(string Denom, ushort Decimals, string[] Aliases);

    public abstract record Coin(string Denom, ulong Amount, bool IsNative)
    {
        internal DenomAmount ToSerialization()
        {
            return new DenomAmount(Denom, Amount);
        }
    }

    public record CoinDecimal(string Denom, decimal Amount, bool IsNative);

    public record CustomCoin(string ContractAddress, ulong Amount) : Coin(ContractAddress, Amount, false);

    public record NativeCoin(string Denom, ulong Amount) : Coin(Denom, Amount, true);

    public record DenomSwapRate(string Denom, decimal SwapRate);
}
