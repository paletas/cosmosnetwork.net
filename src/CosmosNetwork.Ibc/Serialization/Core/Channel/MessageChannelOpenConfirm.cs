using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record MessageChannelOpenConfirm(
        [property: ProtoMember(1, Name = "port_id")] string PortId,
        [property: ProtoMember(2, Name = "channel_id")] string ChannelId,
        [property: ProtoMember(3, Name = "proof_ack")] byte[] ProofAck,
        [property: ProtoMember(4, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(5, Name = "signer")] string Signer) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageChannelOpenConfirm(
                this.PortId,
                this.ChannelId,
                this.ProofAck,
                this.ProofHeight.ToModel(),
                this.Signer);
        }
    }
}
