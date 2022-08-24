using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record MessageChannelOpenTry(
        string PortId,
        string PreviousChannelId,
        Channel Channel,
        string CounterpartyVersion,
        byte[] ProofInit,
        Height ProofHeight,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Channel.MessageChannelOpenTry(
                this.PortId,
                this.PreviousChannelId,
                this.Channel.ToSerialization(),
                this.CounterpartyVersion,
                this.ProofInit,
                this.ProofHeight.ToSerialization(),
                this.Signer);
        }
    }
}
