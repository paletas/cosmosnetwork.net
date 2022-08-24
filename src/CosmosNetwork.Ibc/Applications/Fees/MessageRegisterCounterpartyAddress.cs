using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Applications.Fees
{
    public record MessageRegisterCounterpartyAddress(
        CosmosAddress Address,
        CosmosAddress CounterpartyAddress,
        string ChannelId) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.MessageRegisterCounterpartyAddress(
                Address.Address,
                CounterpartyAddress.Address,
                ChannelId);
        }
    }
}
