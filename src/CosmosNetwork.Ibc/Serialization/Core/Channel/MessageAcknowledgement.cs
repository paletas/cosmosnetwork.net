using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record MessageAcknowledgement(
        [property: ProtoMember(1, Name = "packet")] Packet Packet,
        [property: ProtoMember(2, Name = "acknowledgement")] byte[] Acknowledgement,
        [property: ProtoMember(3, Name = "proof_acked")] byte[] ProofAcknowledged,
        [property: ProtoMember(4, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(5, Name = "signer")] string Signer) : SerializerMessage(Ibc.Core.Channel.MessageAcknowledgement.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageAcknowledgement(
                this.Packet.ToModel(),
                this.Acknowledgement,
                this.ProofAcknowledged,
                this.ProofHeight.ToModel(),
                this.Signer);
        }
    }
}
