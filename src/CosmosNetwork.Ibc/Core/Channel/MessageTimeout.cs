using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageTimeout(
        Packet Packet,
        byte[] ProofUnreceived,
        Height ProofHeight,
        ulong NextSequenceReceiver,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageTimeout(
                this.Packet.ToSerialization(),
                this.ProofUnreceived,
                this.ProofHeight.ToSerialization(),
                this.NextSequenceReceiver,
                this.Signer);
        }
    }
}
