namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    internal record PacketId(
        string PortId,
        string ChannelId,
        ulong Sequence)
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
