using CosmosNetwork.Modules.Authz.Serialization.Json;
using CosmosNetwork.Serialization.Proto;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Authz.Serialization.Authorizations
{
    [JsonConverter(typeof(AuthorizationsConverter))]
    public interface IAuthorization : IHasAny
    {
        new string TypeUrl { get; }

        Authz.Authorizations.IAuthorization ToModel();
    }
}
