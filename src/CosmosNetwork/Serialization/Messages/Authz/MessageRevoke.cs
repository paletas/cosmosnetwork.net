using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Authz
{
    internal record MessageRevoke([property: JsonPropertyName("granter")] string GranterAddress, [property: JsonPropertyName("grantee")] string GranteeAddress, string MessageTypeUrl)
        : SerializerMessage
    {
        internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Authz.MessageRevoke(GranterAddress, GranteeAddress, MessageTypeUrl);
        }
    }
}
