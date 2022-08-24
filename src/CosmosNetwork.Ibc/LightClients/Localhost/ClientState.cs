using CosmosNetwork.Ibc.Core.Client;

namespace CosmosNetwork.Ibc.LightClients.Localhost
{
    public record ClientState(string ChainId, Height Height) : IClientState
    {
        public Serialization.LightClients.IClientState ToSerialization()
        {
            return new Serialization.LightClients.Localhost.ClientState(
                this.ChainId,
                this.Height.ToSerialization());
        }
    }
}
