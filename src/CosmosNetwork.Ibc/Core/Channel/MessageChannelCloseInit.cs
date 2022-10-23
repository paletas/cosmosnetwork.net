using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageChannelCloseInit(
        string PortId,
        string ChannelId,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgChannelCloseInit";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageChannelCloseInit(
                this.PortId,
                this.ChannelId,
                this.Signer);
        }
    }
}
