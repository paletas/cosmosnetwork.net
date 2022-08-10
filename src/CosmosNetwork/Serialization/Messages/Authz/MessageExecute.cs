using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Authz
{
    internal record MessageExecute([property: JsonPropertyName("grantee")] string GranteeAddress, [property: JsonPropertyName("msgs")] SerializerMessage[] Messages)
        : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Authz.MessageExecute(
                GranteeAddress,
                Messages.Select(msg => msg.ToModel()).ToArray());
        }
    }
}
