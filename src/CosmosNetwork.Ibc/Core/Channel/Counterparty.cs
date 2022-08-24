namespace CosmosNetwork.Ibc.Core.Channel
{
    public record Counterparty(
        string PortId,
        string ChannelId)
    {
        internal Serialization.Core.Channel.Counterparty ToSerialization()
        {
            return new Serialization.Core.Channel.Counterparty(this.PortId, this.ChannelId);
        }
    }
}
