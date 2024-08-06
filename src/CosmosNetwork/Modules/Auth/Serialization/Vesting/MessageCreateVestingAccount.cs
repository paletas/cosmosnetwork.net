using CosmosNetwork.Serialization;
using ProtoBuf;
using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Modules.Auth.Serialization.Vesting
{
    [ProtoContract]
    public record MessageCreateVestingAccount(
        [property: ProtoMember(1, Name = "from_address")] string FromAddress,
        [property: ProtoMember(2, Name = "to_address")] string ToAddress,
        [property: ProtoMember(3, Name = "amount")] DenomAmount[] Amount,
        [property: ProtoMember(6, Name = "start_time")] Timestamp StartTime,
        [property: ProtoMember(4, Name = "end_time")] Timestamp EndTime,
        [property: ProtoMember(5, Name = "delayed")] bool Delayed) : SerializerMessage(Auth.Vesting.MessageCreateVestingAccount.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Auth.Vesting.MessageCreateVestingAccount(
                this.TypeUrl,
                this.FromAddress,
                this.ToAddress,
                this.Amount.Select(x => x.ToModel()).ToArray(),
                this.StartTime,
                this.EndTime,
                this.Delayed);
        }
    }
}