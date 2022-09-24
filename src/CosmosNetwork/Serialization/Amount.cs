using UltimateOrb;

namespace CosmosNetwork.Serialization
{
    public record DenomAmount(string Denom, UInt128 Amount)
    {
        public virtual Coin ToModel()
        {
            return new NativeCoin(Denom, Amount);
        }
    };

    public record CustomTokenAmount(string ContractAddress, UInt128 Amount) : DenomAmount(ContractAddress, Amount)
    {
        public override Coin ToModel()
        {
            return new CustomCoin(ContractAddress, Amount);
        }
    }

    public record NativeTokenAmount(string Denom, UInt128 Amount) : DenomAmount(Denom, Amount)
    {
        public override Coin ToModel()
        {
            return new NativeCoin(Denom, Amount);
        }
    }
}
