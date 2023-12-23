using CosmosNetwork.Ibc.LightClients;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Client
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageSubmitMisbehaviour(
        string ClientId,
        IMisbehaviour Misbehaviour,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.client.v1.MsgSubmitMisbehaviour";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Client.MessageSubmitMisbehaviour(this.ClientId, this.Signer)
            {
                Misbehaviour = this.Misbehaviour.ToSerialization()
            };
        }
    }
}
