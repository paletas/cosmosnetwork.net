using CosmosNetwork.Ibc.LightClients;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Client
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageCreateClient(
        IClientState ClientState,
        IConsensusState ConsensusState,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.client.v1.MsgCreateClient";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Client.MessageCreateClient(this.Signer)
            {
                ClientState = this.ClientState.ToSerialization(),
                ConsensusState = this.ConsensusState.ToSerialization()
            };
        }
    }
}
