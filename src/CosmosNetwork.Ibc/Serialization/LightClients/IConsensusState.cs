using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Ibc.Serialization.LightClients
{
    public interface IConsensusState : IHasAny
    {
        Ibc.LightClients.IConsensusState ToModel();
    }
}
