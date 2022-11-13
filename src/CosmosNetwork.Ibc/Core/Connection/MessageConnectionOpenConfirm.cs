using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Connection
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageConnectionOpenConfirm(
        string ConnectionId,
        byte[] ProofAck,
        Height ProofHeight,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.connection.v1.MsgConnectionOpenConfirm";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Connection.MessageConnectionOpenConfirm(
                this.ConnectionId,
                this.ProofAck,
                this.ProofHeight.ToSerialization(),
                this.Signer);
        }
    }
}
