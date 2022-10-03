using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record MessageChannelOpenInit(
        [property: ProtoMember(1, Name = "port_id")] string PortId,
        [property: ProtoMember(2, Name = "channel")] Channel Channel,
        [property: ProtoMember(3, Name = "signer")] string Signer) : SerializerMessage(Ibc.Core.Channel.MessageChannelOpenInit.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageChannelOpenInit(
                this.PortId,
                this.Channel.ToModel(),
                this.Signer);
        }
    }
}
