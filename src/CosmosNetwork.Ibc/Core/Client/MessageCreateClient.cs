using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Client
{
    public record MessageCreateClient(
        byte[] ClientState,
        byte[] ConsensusState,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Client.MessageCreateClient(this.Signer)
            {
                ClientState = this.ClientState,
                ConsensusState = this.ConsensusState
            };
        }
    }
}
