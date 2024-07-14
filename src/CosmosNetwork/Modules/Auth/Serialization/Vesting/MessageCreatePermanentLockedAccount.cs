using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Auth.Serialization.Vesting
{
    [ProtoContract]
    public record MessageCreatePermanentLockedAccount(
        [property: ProtoMember(1, Name = "from_address")] string FromAddress,
        [property: ProtoMember(2, Name = "to_address")] string ToAddress,
        [property: ProtoMember(3, Name = "amount")] DenomAmount[] Amount) : SerializerMessage(Auth.Vesting.MessageCreatePermanentLockedAccount.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Auth.Vesting.MessageCreatePermanentLockedAccount(
                this.FromAddress,
                this.ToAddress,
                this.Amount.Select(x => x.ToModel()).ToArray());
        }
    }
}
