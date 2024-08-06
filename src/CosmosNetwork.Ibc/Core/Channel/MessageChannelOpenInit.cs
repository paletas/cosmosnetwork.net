using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageChannelOpenInit(
        string PortId,
        Channel Channel,
        string Signer) : Message(COSMOS_DESCRIPTOR)
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgChannelOpenInit";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageChannelOpenInit(
                this.PortId,
                this.Channel.ToSerialization(),
                this.Signer);
        }
    }
}
