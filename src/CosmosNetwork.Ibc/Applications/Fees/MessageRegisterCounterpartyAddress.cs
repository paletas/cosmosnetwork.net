using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Applications.Fees
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageRegisterCounterpartyAddress(
        CosmosAddress Address,
        CosmosAddress CounterpartyAddress,
        string ChannelId) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.applications.fee.v1.MsgRegisterCounterpartyAddress";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.Fees.MessageRegisterCounterpartyAddress(
                Address.Address,
                CounterpartyAddress.Address,
                ChannelId);
        }
    }
}
