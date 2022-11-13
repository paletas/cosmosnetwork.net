using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageChannelCloseConfirm(
        string PortId,
        string ChannelId,
        byte[] ProofInit,
        Height ProofHeight,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.channel.v1.MsgChannelCloseConfirm";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageChannelCloseConfirm(
                this.PortId,
                this.ChannelId,
                this.ProofInit,
                this.ProofHeight.ToSerialization(),
                this.Signer);
        }
    }
}
