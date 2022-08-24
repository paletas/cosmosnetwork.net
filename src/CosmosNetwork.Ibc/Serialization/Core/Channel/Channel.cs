namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    internal record Channel(
        StateEnum State,
        OrderEnum Ordering,
        Counterparty Counterparty,
        string[] ConnectionHops,
        string Version)
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
