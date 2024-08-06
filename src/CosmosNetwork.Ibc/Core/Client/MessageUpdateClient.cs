using CosmosNetwork.Ibc.LightClients;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Client
{
    public record MessageUpdateClient(
        string ClientId,
        IHeader Header,
        string Signer) : Message(COSMOS_DESCRIPTOR)
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.client.v1.MsgUpdateClient";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Client.MessageUpdateClient(this.ClientId, this.Signer)
            {
                Header = this.Header.ToSerialization()
            };
        }
    }
}
