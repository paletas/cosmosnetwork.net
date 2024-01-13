using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    public record MessageChannelCloseInit(
        [property: ProtoMember(1, Name = "port_id")] string PortId,
        [property: ProtoMember(2, Name = "channel_id")] string ChannelId,
        [property: ProtoMember(3, Name = "signer")] string Signer) : SerializerMessage(Ibc.Core.Channel.MessageChannelCloseInit.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageChannelCloseInit(
                this.PortId,
                this.ChannelId,
                this.Signer);
        }
    }
}
