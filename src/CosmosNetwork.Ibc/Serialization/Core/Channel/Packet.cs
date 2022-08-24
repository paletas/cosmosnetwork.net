using CosmosNetwork.Ibc.Serialization.Core.Client;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    internal record Packet(
        ulong Sequence,
        string SourcePort,
        string SourceChannel,
        string DestinationPort,
        string DestinationChannel,
        byte[] Data,
        Height TimeoutHeight,
        ulong TimeoutTimestamp)
    {
        public Ibc.Core.Channel.Packet ToModel()
        {
            return new Ibc.Core.Channel.Packet(
                this.Sequence,
                this.SourcePort,
                this.SourceChannel,
                this.DestinationPort,
                this.DestinationChannel,
                this.Data,
                this.TimeoutHeight.ToModel(),
                this.TimeoutTimestamp);
        }
    }
}
