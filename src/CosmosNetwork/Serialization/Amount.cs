namespace CosmosNetwork.Serialization
{
    public record DenomAmount(string Denom, ulong Amount)
    {
        public virtual Coin ToModel()
        {
            return new NativeCoin(Denom, Amount);
        }
    };

    public record CustomTokenAmount(string ContractAddress, ulong Amount) : DenomAmount(ContractAddress, Amount)
    {
        public override Coin ToModel()
        {
            return new CustomCoin(ContractAddress, Amount);
        }
    }

    public record NativeTokenAmount(string Denom, ulong Amount) : DenomAmount(Denom, Amount)
    {
        public override Coin ToModel()
        {
            return new NativeCoin(Denom, Amount);
        }
    }
}
