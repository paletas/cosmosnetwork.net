using CosmosNetwork.Ibc.Serialization.Json;
using CosmosNetwork.Serialization.Proto;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.LightClients
{
    [JsonConverter(typeof(MisbehaviourConverter))]
    public interface IMisbehaviour : IHasAny
    {
        Ibc.LightClients.IMisbehaviour ToModel();
    }
}
