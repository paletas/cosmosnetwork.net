using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record MessageTimeoutOnClose(
        [property: ProtoMember(1, Name = "packet")] Packet Packet,
        [property: ProtoMember(2, Name = "proof_unreceived")] byte[] ProofUnreceived,
        [property: ProtoMember(3, Name = "proof_close")] byte[] ProofClose,
        [property: ProtoMember(4, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(5, Name = "next_sequence_recv"), JsonPropertyName("next_sequence_recv")] ulong NextSequenceReceiver,
        [property: ProtoMember(6, Name = "signer")] string Signer) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageTimeoutOnClose(
                this.Packet.ToModel(),
                this.ProofUnreceived,
                this.ProofClose,
                this.ProofHeight.ToModel(),
                this.NextSequenceReceiver,
                this.Signer);
        }
    }
}
