using ProtoBuf;

namespace CosmosNetwork.Serialization
{
  [ProtoContract]
    public record DenomAmount(
        [property: ProtoMember(1, Name = "denom")] string Denom,
        [property: ProtoMember(2, Name = "amount")] string Amount)
    {
        public virtual Coin ToModel()
        {
            return UInt128.TryParse(this.Amount, out UInt128 amount) == false
                ? throw new InvalidOperationException($"invalid amount format: {this.Amount}")
                : (Coin)new NativeCoin(this.Denom, amount);
        }

        public virtual CoinDecimal ToDecimalModel()
        {
            return decimal.TryParse(this.Amount, out decimal amount) == false
                ? throw new InvalidOperationException($"invalid amount format: {this.Amount}")
                : new CoinDecimal(this.Denom, amount, true);
        }
    };
}
