using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record MessageChannelOpenTry(
        [property: ProtoMember(1, Name = "port_id")] string PortId,
        [property: ProtoMember(1, Name = "previous_channel_id")] string PreviousChannelId,
        [property: ProtoMember(1, Name = "channel")] Channel Channel,
        [property: ProtoMember(1, Name = "counterparty_version")] string CounterpartyVersion,
        [property: ProtoMember(1, Name = "proof_init")] byte[] ProofInit,
        [property: ProtoMember(1, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(1, Name = "signer")] string Signer) : SerializerMessage
    {
        protected override Message ToModel()
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
