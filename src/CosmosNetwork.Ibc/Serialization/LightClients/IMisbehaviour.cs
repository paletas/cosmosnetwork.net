using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Ibc.Serialization.LightClients
{
    public interface IMisbehaviour : IHasAny
    {
        Ibc.LightClients.IMisbehaviour ToModel();
    }
}
