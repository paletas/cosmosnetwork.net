using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Applications.Fees
{
    [ProtoContract]
    internal record MessageRegisterCounterpartyAddress(
        [property: ProtoMember(1, Name = "address")] string Address,
        [property: ProtoMember(2, Name = "counterparty_address")] string CounterpartyAddress,
        [property: ProtoMember(3, Name = "channel_id")] string ChannelId) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Ibc.Applications.Fees.MessageRegisterCounterpartyAddress(
                Address,
                CounterpartyAddress,
                ChannelId);
        }
    }
}
