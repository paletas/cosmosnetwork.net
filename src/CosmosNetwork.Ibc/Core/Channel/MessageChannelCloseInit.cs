using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageChannelCloseInit(
        string PortId,
        string ChannelId,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageChannelCloseInit(
                this.PortId,
                this.ChannelId,
                this.Signer);
        }
    }
}
