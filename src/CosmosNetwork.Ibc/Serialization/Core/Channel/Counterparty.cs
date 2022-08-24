using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record Counterparty(
        [property: ProtoMember(1, Name = "port_id")] string PortId,
        [property: ProtoMember(2, Name = "channel_id")] string ChannelId)
    {
        public Ibc.Core.Channel.Counterparty ToModel()
        {
            return new Ibc.Core.Channel.Counterparty(this.PortId, this.ChannelId);
        }
    }
}
