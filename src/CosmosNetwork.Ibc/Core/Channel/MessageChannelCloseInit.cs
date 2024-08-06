using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageChannelCloseInit(
        string PortId,
        string ChannelId,
        string Signer) : Message(COSMOS_DESCRIPTOR)
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgChannelCloseInit";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageChannelCloseInit(
                this.PortId,
                this.ChannelId,
                this.Signer);
        }
    }
}
