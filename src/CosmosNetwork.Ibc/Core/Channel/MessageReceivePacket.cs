using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageReceivePacket(
        Packet Packet,
        byte[] ProofCommitment,
        Height ProofHeight,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgRecvPacket";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageReceivePacket(
                this.Packet.ToSerialization(),
                this.ProofCommitment,
                this.ProofHeight.ToSerialization(),
                this.Signer);
        }
    }
}
