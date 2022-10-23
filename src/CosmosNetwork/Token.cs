using CosmosNetwork.Serialization;
using UltimateOrb;

namespace CosmosNetwork
{
    public record DenomMetadata(string Description, DenomUnit[] Units, string BaseDenom, string DisplayDenom, string Name, string Symbol);

    public record DenomUnit(string Denom, ushort Decimals, string[] Aliases);

    public abstract record Coin(string Denom, UInt128 Amount, bool IsNative)
    {
        public DenomAmount ToSerialization()
        {
            return new DenomAmount(this.Denom, this.Amount.ToStringCStyleU128());
        }
    }

    public record CoinDecimal(string Denom, decimal Amount, bool IsNative);

    public record CustomCoin(string ContractAddress, UInt128 Amount) : Coin(ContractAddress, Amount, false);

    public record NativeCoin(string Denom, UInt128 Amount) : Coin(Denom, Amount, true);

    public record DenomSwapRate(string Denom, decimal SwapRate);
}
