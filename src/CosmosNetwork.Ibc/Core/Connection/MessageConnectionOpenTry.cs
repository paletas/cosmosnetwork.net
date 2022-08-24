using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Connection
{
    public record MessageConnectionOpenTry(
        string ClientId,
        string PreviousConnectionId,
        byte[] ClientState,
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
                ClientState = this.ClientState
            };
        }
    }
}
