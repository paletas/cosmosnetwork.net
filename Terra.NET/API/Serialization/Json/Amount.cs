using Cosmos.SDK.Protos;

namespace Terra.NET.API.Serialization.Json
{
    internal record DenomAmount(string Denom, ulong Amount)
    {
        internal Cosmos.SDK.Protos.Coin ToProto()
        {
            return new Cosmos.SDK.Protos.Coin
            {
                Denom = Denom,
                Amount = Amount.ToString()
            };
        }

        internal virtual Terra.NET.Coin ToModel()
        {
            return new Terra.NET.Coin(this.Denom, this.Amount, true);
        }
    };

    internal record CustomTokenAmount(string ContractAddress, ulong Amount) : DenomAmount(ContractAddress, Amount)
    {
        internal override NET.Coin ToModel()
        {
            return new Terra.NET.CustomCoin(this.ContractAddress, this.Amount);
        }
    }

    internal record NativeTokenAmount(string Denom, ulong Amount) : DenomAmount(Denom, Amount)
    {
        internal override NET.Coin ToModel()
        {
            return new Terra.NET.NativeCoin(this.Denom, this.Amount);
        }
    }
}
