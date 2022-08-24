using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    internal record MessageAcknowledgement(
        Packet Packet,
        byte[] Acknowledgement,
        byte[] ProofAcknowledged,
        Height ProofHeight,
        string Signer) : SerializerMessage
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
