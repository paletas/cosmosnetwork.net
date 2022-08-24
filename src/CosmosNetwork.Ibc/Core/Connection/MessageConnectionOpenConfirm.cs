using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Connection
{
    public record MessageConnectionOpenConfirm(
        string ConnectionId,
        byte[] ProofAck,
        Height ProofHeight,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Connection.MessageConnectionOpenConfirm(
                this.ConnectionId,
                this.ProofAck,
                this.ProofHeight.ToSerialization(),
                this.Signer);
        }
    }
}
