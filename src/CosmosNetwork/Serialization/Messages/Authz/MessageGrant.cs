using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Authz
{
    internal record MessageGrant(
        [property: JsonPropertyName("granter")] string GranterAddress,
        [property: JsonPropertyName("grantee")] string GranteeAddress)
        : SerializerMessage
    {
        internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Authz.MessageGrant(
                GranterAddress,
                GranteeAddress);
        }
    }
}
