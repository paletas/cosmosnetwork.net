using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Ibc.LightClients;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Connection
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageConnectionOpenTry(
        string ClientId,
        string PreviousConnectionId,
        IClientState ClientState,
        Counterparty Counterparty,
        ulong DelayPeriod,
        Version[] CounterpartyVersions,
        Height ProofHeight,
        byte[] ProofInit,
        byte[] ProofClient,
        byte[] ProofConsensus,
        Height ConsensusHeight,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.connection.v1.MsgConnectionOpenTry";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Connection.MessageConnectionOpenTry(
                this.ClientId,
                this.PreviousConnectionId,
                this.Counterparty.ToSerialization(),
                this.DelayPeriod,
                this.CounterpartyVersions.Select(cv => cv.ToSerialization()).ToArray(),
                this.ProofHeight.ToSerialization(),
                this.ProofInit,
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
