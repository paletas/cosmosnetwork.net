using CosmosNetwork.Ibc.LightClients;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Client
{
    public record MessageCreateClient(
        IClientState ClientState,
        IConsensusState ConsensusState,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Client.MessageCreateClient(this.Signer)
            {
                ClientState = this.ClientState.ToSerialization(),
                ConsensusState = this.ConsensusState.ToSerialization()
            };
        }
    }
}
