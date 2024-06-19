using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Authz.Serialization.Authorizations
{
    internal class GenericAuthorization : IAuthorization
    {
        [JsonPropertyName("msg")]
        public string TypeUrl { get; set; }

        public Authz.Authorizations.IAuthorization ToModel()
        {
            return new Authz.Authorizations.GenericAuthorization(this.TypeUrl);
        }
    }
}
