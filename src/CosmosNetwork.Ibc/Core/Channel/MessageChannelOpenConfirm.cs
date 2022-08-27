using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageChannelOpenConfirm(
        string PortId,
        string ChannelId,
        byte[] ProofAck,
        Height ProofHeight,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgChannelOpenConfirm";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageChannelOpenConfirm(
                this.PortId,
                this.ChannelId,
                this.ProofAck,
                this.ProofHeight.ToSerialization(),
                this.Signer);
        }
    }
}
