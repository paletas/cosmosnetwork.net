using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Ibc.Serialization.LightClients
{
    public interface IHeader : IHasAny
    {
        Ibc.LightClients.IHeader ToModel();
    }
}
