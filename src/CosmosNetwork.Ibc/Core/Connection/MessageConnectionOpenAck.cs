using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Ibc.LightClients;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Connection
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageConnectionOpenAck(
        string ConnectionId,
        string PreviousConnectionId,
        Version Version,
        IClientState ClientState,
        Height ProofHeight,
        byte[] ProofTry,
        byte[] ProofClient,
        byte[] ProofConsensus,
        Height ConsensusHeight,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.connection.v1.MsgConnectionOpenAck";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Connection.MessageConnectionOpenAck(
                this.ConnectionId,
                this.PreviousConnectionId,
                this.Version.ToSerialization(),
                this.ProofHeight.ToSerialization(),
                this.ProofTry,
                this.ProofClient,
                this.ProofConsensus,
                this.ConsensusHeight.ToSerialization(),
                this.Signer)
            {
                ClientState = this.ClientState.ToSerialization()
            };
        }
    }
}
