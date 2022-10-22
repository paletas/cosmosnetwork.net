namespace CosmosNetwork.Ibc.Core.Channel
{
    public record PacketId(
        string PortId,
        string ChannelId,
        ulong Sequence)
    {
        internal Serialization.Core.Channel.PacketId ToSerialization()
        {
            return new Serialization.Core.Channel.PacketId(
                this.PortId,
                this.ChannelId,
                this.Sequence);
        }
    }
}
