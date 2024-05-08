using CosmosNetwork.Ibc.Core.Client;
using System.Text;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record Packet(
        ulong Sequence,
        string SourcePort,
        string SourceChannel,
        string DestinationPort,
        string DestinationChannel,
        string Data,
        Height TimeoutHeight,
        ulong TimeoutTimestamp)
    {
        internal Serialization.Core.Channel.Packet ToSerialization()
        {
            return new Serialization.Core.Channel.Packet(
                this.Sequence,
                this.SourcePort,
                this.SourceChannel,
                this.DestinationPort,
                this.DestinationChannel,
                Encoding.UTF8.GetBytes(this.Data),
                this.TimeoutHeight.ToSerialization(),
                this.TimeoutTimestamp);
        }
    }
}
