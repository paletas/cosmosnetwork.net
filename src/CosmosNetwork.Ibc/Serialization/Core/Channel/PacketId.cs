using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record PacketId(
        [property: ProtoMember(1, Name = "port_id")] string PortId,
        [property: ProtoMember(2, Name = "channel_id")] string ChannelId,
        [property: ProtoMember(3, Name = "sequence")] ulong Sequence)
    {
        public Ibc.Core.Channel.PacketId ToModel()
        {
            return new Ibc.Core.Channel.PacketId(
                this.PortId,
                this.ChannelId,
                this.Sequence);
        }
    }
}
