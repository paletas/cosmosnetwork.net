using CosmosNetwork.Ibc.Core.Client;

namespace CosmosNetwork.Ibc.LightClients.Localhost
{
    public record ClientState(string ChainId, Height Height) : IClientState
    {
        public ulong GetHeight()
        {
            return this.Height.RevisionHeight;
        }

        public Serialization.LightClients.IClientState ToSerialization()
        {
            return new Serialization.LightClients.Localhost.ClientState(
                this.ChainId,
                this.Height.ToSerialization());
        }
    }
}
