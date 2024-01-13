using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record MessageChannelOpenTry(
        [property: ProtoMember(1, Name = "port_id")] string PortId,
        [property: ProtoMember(2, Name = "previous_channel_id")] string PreviousChannelId,
        [property: ProtoMember(3, Name = "channel")] Channel Channel,
        [property: ProtoMember(4, Name = "counterparty_version")] string CounterpartyVersion,
        [property: ProtoMember(5, Name = "proof_init")] byte[] ProofInit,
        [property: ProtoMember(6, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(7, Name = "signer")] string Signer) : SerializerMessage(Ibc.Core.Channel.MessageChannelOpenTry.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageChannelOpenTry(
                this.PortId,
                this.PreviousChannelId,
                this.Channel.ToModel(),
                this.CounterpartyVersion,
                this.ProofInit,
                this.ProofHeight.ToModel(),
                this.Signer);
        }
    }
}
