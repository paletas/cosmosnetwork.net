using CosmosNetwork.Ibc.Serialization.Json;
using CosmosNetwork.Serialization.Proto;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.LightClients
{
    [JsonConverter(typeof(HeaderConverter))]
    public interface IHeader : IHasAny
    {
        Ibc.LightClients.IHeader ToModel();
    }
}
