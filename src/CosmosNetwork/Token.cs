using CosmosNetwork.Serialization;

namespace CosmosNetwork
{
    public abstract record Coin(string Denom, UInt128 Amount, bool IsNative)
    {
        public DenomAmount ToSerialization()
        {
            return new DenomAmount(this.Denom, this.Amount.ToString());
        }
    }

    public record CoinDecimal(string Denom, decimal Amount, bool IsNative)
    {
        public DenomAmount ToSerialization()
        {
            return new DenomAmount(this.Denom, this.Amount.ToString());
        }
    }

    public record CustomCoin(string ContractAddress, UInt128 Amount) : Coin(ContractAddress, Amount, false);

    public record NativeCoin(string Denom, UInt128 Amount) : Coin(Denom, Amount, true);

    public record DenomSwapRate(string Denom, decimal SwapRate);
}
