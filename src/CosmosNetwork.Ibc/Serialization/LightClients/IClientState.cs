using CosmosNetwork.Ibc.Serialization.Json;
using CosmosNetwork.Serialization.Proto;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.LightClients
{
    [JsonConverter(typeof(ClientStateConverter))]
    public interface IClientState : IHasAny
    {
        Ibc.LightClients.IClientState ToModel();
    }
}
