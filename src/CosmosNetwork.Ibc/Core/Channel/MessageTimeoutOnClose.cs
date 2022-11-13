using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageTimeoutOnClose(
        Packet Packet,
        byte[] ProofUnreceived,
        byte[] ProofClose,
        Height ProofHeight,
        ulong NextSequenceReceiver,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgTimeoutOnClose";

        public override SerializerMessage ToSerialization()
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
