using CosmosNetwork.Ibc.Serialization.Json;
using CosmosNetwork.Serialization.Proto;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.LightClients
{
    [JsonConverter(typeof(ConsensusStateConverter))]
    public interface IConsensusState : IHasAny
    {
        Ibc.LightClients.IConsensusState ToModel();
    }
}
