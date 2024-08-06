using CosmosNetwork.Serialization;
using ProtoBuf;
using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Modules.Auth.Serialization.Vesting
{
    [ProtoContract]
    public record MessageCreatePeriodicVestingAccount(
        [property: ProtoMember(1, Name = "from_address")] string FromAddress,
        [property: ProtoMember(2, Name = "to_address")] string ToAddress,
        [property: ProtoMember(3, Name = "start_time")] Timestamp StartTime,
        [property: ProtoMember(4, Name = "vesting_periods")] VestingPeriod[] VestingPeriods) : SerializerMessage(Auth.Vesting.MessageCreatePeriodicVestingAccount.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Auth.Vesting.MessageCreatePeriodicVestingAccount(
                this.TypeUrl,
                this.FromAddress,
                this.ToAddress,
                this.StartTime,
                this.VestingPeriods.Select(x => x.ToModel()).ToArray());
        }
    }
}
