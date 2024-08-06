using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageTimeout(
        Packet Packet,
        byte[] ProofUnreceived,
        Height ProofHeight,
        ulong NextSequenceReceiver,
        string Signer) : Message(COSMOS_DESCRIPTOR)
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgTimeout";

        public override SerializerMessage ToSerialization()
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
