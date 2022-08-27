using CosmosNetwork.Ibc.LightClients;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Client
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageUpdateClient(
        string ClientId,
        IHeader Header,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.client.v1.MsgUpdateClient";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Client.MessageUpdateClient(this.ClientId, this.Signer)
            {
                Header = this.Header.ToSerialization()
            };
        }
    }
}
