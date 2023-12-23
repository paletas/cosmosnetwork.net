using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageChannelOpenInit(
        string PortId,
        Channel Channel,
        string Signer) : Message
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
