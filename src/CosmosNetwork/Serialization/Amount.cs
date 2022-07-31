namespace CosmosNetwork.Serialization
{
    internal record DenomAmount(string Denom, ulong Amount)
    {
        internal virtual Coin ToModel()
        {
            return new NativeCoin(Denom, Amount);
        }
    };

    internal record CustomTokenAmount(string ContractAddress, ulong Amount) : DenomAmount(ContractAddress, Amount)
    {
        internal override Coin ToModel()
        {
            return new CustomCoin(ContractAddress, Amount);
        }
    }

    internal record NativeTokenAmount(string Denom, ulong Amount) : DenomAmount(Denom, Amount)
    {
        internal override Coin ToModel()
        {
            return new NativeCoin(Denom, Amount);
        }
    }
}
