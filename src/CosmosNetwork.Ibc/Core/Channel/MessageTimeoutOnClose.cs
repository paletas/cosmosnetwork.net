using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageTimeoutOnClose(
        Packet Packet,
        byte[] ProofUnreceived,
        byte[] ProofClose,
        Height ProofHeight,
        ulong NextSequenceReceiver,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageTimeoutOnClose(
                this.Packet.ToSerialization(),
                this.ProofUnreceived,
                this.ProofClose,
                this.ProofHeight.ToSerialization(),
                this.NextSequenceReceiver,
                this.Signer);
        }
    }
}
