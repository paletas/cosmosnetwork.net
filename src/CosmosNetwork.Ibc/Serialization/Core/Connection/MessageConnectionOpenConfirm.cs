using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Connection
{
    [ProtoContract]
    internal record MessageConnectionOpenConfirm(
        [property: ProtoMember(1, Name = "connection_id")] string ConnectionId,
        [property: ProtoMember(2, Name = "proof_ack")] byte[] ProofAck,
        [property: ProtoMember(3, Name = "proof_height")] Height ProofHeight,
        [property: ProtoMember(4, Name = "signer")] string Signer) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Ibc.Core.Connection.MessageConnectionOpenConfirm(
                this.ConnectionId,
                this.ProofAck,
                this.ProofHeight.ToModel(),
                this.Signer);
        }
    }
}
