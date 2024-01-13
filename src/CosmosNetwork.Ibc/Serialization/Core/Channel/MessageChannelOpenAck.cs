using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record MessageChannelOpenAck(
        [property: ProtoMember(1, Name = "port_id")] string PortId,
        [property: ProtoMember(2, Name = "channel_id")] string ChannelId,
        [property: ProtoMember(3, Name = "counterparty_channel_id")] string CounterpartyChannelId,
        [property: ProtoMember(4, Name = "counterparty_version")] string CounterpartyVersion,
        [property: ProtoMember(5, Name = "proof_try")] byte[] ProofTry,
        [property: ProtoMember(6, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(7, Name = "signer")] string Signer) : SerializerMessage(Ibc.Core.Channel.MessageChannelOpenAck.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageChannelOpenAck(
                this.PortId,
                this.ChannelId,
                this.CounterpartyChannelId,
                this.CounterpartyVersion,
                this.ProofTry,
                this.ProofHeight.ToModel(),
                this.Signer);
        }
    }
}
