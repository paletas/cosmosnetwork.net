using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record Channel(
        [property: ProtoMember(1, Name = "state")] StateEnum State,
        [property: ProtoMember(2, Name = "ordering")] OrderEnum Ordering,
        [property: ProtoMember(3, Name = "counterparty")] Counterparty Counterparty,
        [property: ProtoMember(4, Name = "connection_hops")] string[] ConnectionHops,
        [property: ProtoMember(5, Name = "version")] string Version)
    {
        public Ibc.Core.Channel.Channel ToModel()
        {
            return new Ibc.Core.Channel.Channel(
                (Ibc.Core.Channel.StateEnum)this.State,
                (Ibc.Core.Channel.OrderEnum)this.Ordering,
                this.Counterparty.ToModel(),
                this.ConnectionHops,
                this.Version);
        }
    }
}
