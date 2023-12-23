using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Applications.Fees
{
    [ProtoContract]
    internal record MessageRegisterCounterpartyAddress(
        [property: ProtoMember(1, Name = "address")] string Address,
        [property: ProtoMember(2, Name = "counterparty_address")] string CounterpartyAddress,
        [property: ProtoMember(3, Name = "channel_id")] string ChannelId) : SerializerMessage(Ibc.Applications.Fees.MessageRegisterCounterpartyAddress.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            return new Ibc.Applications.Fees.MessageRegisterCounterpartyAddress(
                this.Address,
                this.CounterpartyAddress,
                this.ChannelId);
        }
    }
}
