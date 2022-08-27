using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageAcknowledgement(
        Packet Packet,
        byte[] Acknowledgement,
        byte[] ProofAcknowledged,
        Height ProofHeight,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgAcknowledgement";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageAcknowledgement(
                this.Packet.ToSerialization(),
                this.Acknowledgement,
                this.ProofAcknowledged,
                this.ProofHeight.ToSerialization(),
                this.Signer);
        }
    }
}
