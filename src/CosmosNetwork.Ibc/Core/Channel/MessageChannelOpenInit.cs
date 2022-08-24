using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageChannelOpenInit(
        string PortId,
        Channel Channel,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageChannelOpenInit(
                this.PortId,
                this.Channel.ToSerialization(),
                this.Signer);
        }
    }
}
