using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record MessageChannelCloseConfirm(
        [property: ProtoMember(1, Name = "port_id")] string PortId,
        [property: ProtoMember(2, Name = "channel_id")] string ChannelId,
        [property: ProtoMember(3, Name = "proof_init")] byte[] ProofInit,
        [property: ProtoMember(4, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(5, Name = "signer")] string Signer) : SerializerMessage(Ibc.Core.Channel.MessageChannelCloseConfirm.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageChannelCloseConfirm(
                this.PortId,
                this.ChannelId,
                this.ProofInit,
                this.ProofHeight.ToModel(),
                this.Signer);
        }
    }
}
