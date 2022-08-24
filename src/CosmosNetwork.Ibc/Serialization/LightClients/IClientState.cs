using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Ibc.Serialization.LightClients
{
    public interface IClientState : IHasAny
    {
        Ibc.LightClients.IClientState ToModel();
    }
}
