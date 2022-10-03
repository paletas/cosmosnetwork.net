using CosmosNetwork.Ibc.Serialization.Core.Client;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record Packet(
        [property: ProtoMember(1, Name = "sequence")] ulong Sequence,
        [property: ProtoMember(2, Name = "source_port")] string SourcePort,
        [property: ProtoMember(3, Name = "source_channel")] string SourceChannel,
        [property: ProtoMember(4, Name = "destination_port")] string DestinationPort,
        [property: ProtoMember(5, Name = "destination_channel")] string DestinationChannel,
        [property: ProtoMember(6, Name = "data")] byte[] Data,
        [property: ProtoMember(7, Name = "timeout_height")] Height TimeoutHeight,
        [property: ProtoMember(8, Name = "timeout_timestamp")] ulong TimeoutTimestamp)
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
