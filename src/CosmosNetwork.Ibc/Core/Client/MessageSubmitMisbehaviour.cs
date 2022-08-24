using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Client
{
    public record MessageSubmitMisbehaviour(
        string ClientId,
        byte[] Misbehaviour,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Client.MessageSubmitMisbehaviour(this.ClientId, this.Signer)
            {
                Misbehaviour = this.Misbehaviour
            };
        }
    }
}
