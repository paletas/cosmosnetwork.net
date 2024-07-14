using CosmosNetwork.Serialization;
using ProtoBuf;
using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Modules.Auth.Serialization
{
    [ProtoContract]
    public record VestingPeriod(
        [property: ProtoMember(1, Name = "length")] long Length,
        [property: ProtoMember(2, Name = "amount")] DenomAmount[] Amount)
    {
        public Auth.VestingPeriod ToModel()
        {
            return new Auth.VestingPeriod(
                TimeSpan.FromSeconds(this.Length),
                this.Amount.Select(c => c.ToModel()).ToArray());
        }
    }
}
