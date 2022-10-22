using ProtoBuf;
using UltimateOrb;

namespace CosmosNetwork.Serialization
{
    [ProtoContract]
    public record DenomAmount(
        [property: ProtoMember(1, Name = "denom")] string Denom,
        [property: ProtoMember(2, Name = "amount")] string Amount)
    {
        public virtual Coin ToModel()
        {
            UInt128 amount;
            if (UInt128.TryParseCStyleNormalizedU128(Amount, out amount) == false)
                throw new InvalidOperationException($"invalid amount format: {Amount}");

            return new NativeCoin(Denom, amount);
        }
    };
}
