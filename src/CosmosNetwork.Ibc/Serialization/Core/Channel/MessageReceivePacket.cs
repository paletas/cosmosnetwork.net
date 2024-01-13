using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    [ProtoContract]
    internal record MessageReceivePacket(
        [property: ProtoMember(1, Name = "packet")] Packet Packet,
        [property: ProtoMember(2, Name = "proof_commitment")] byte[] ProofCommitment,
        [property: ProtoMember(3, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(4, Name = "signer")] string Signer) : SerializerMessage(Ibc.Core.Channel.MessageReceivePacket.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageReceivePacket(
                this.Packet.ToModel(),
                this.ProofCommitment,
                this.ProofHeight.ToModel(),
                this.Signer);
        }
    }
}
