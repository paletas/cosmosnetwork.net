using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageChannelOpenAck(
        string PortId,
        string ChannelId,
        string CounterpartyChannelId,
        string CounterpartyVersion,
        byte[] ProofTry,
        Height ProofHeight,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageChannelOpenAck(
                this.PortId,
                this.ChannelId,
                this.CounterpartyChannelId,
                this.CounterpartyVersion,
                this.ProofTry,
                this.ProofHeight.ToSerialization(),
                this.Signer);
        }
    }
}
