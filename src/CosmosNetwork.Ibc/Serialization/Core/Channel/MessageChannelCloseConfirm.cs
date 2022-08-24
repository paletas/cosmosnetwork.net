using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Serialization.Core.Channel
{
    internal record MessageChannelCloseConfirm(
        string PortId,
        string ChannelId,
        byte[] ProofInit,
        Height ProofHeight,
        string Signer) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Ibc.Core.Channel.MessageChannelCloseConfirm(
                this.PortId,
                this.ChannelId,
                this.ProofInit,
                this.ProofHeight.ToModel(),
                this.Signer);
        }
    }
}
