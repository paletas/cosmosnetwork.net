using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.FeeGrant
{
    internal record MessageRevokeAllowance(
        [property: JsonPropertyName("granter")] string GranterAddress,
        [property: JsonPropertyName("grantee")] string GranteeAddress) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "feegrant/MsgRevokeAllowance";

        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.FeeGrant.MessageRevokeAllowance(GranterAddress, GranteeAddress);
        }
    }
}
