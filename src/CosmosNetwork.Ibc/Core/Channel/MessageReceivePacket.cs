using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageReceivePacket(
        Packet Packet,
        byte[] ProofCommitment,
        Height ProofHeight,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageReceivePacket(
                this.Packet.ToSerialization(),
                this.ProofCommitment,
                this.ProofHeight.ToSerialization(),
                this.Signer);
        }
    }
}
