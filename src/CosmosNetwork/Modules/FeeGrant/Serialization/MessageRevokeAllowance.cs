using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.FeeGrant.Serialization
{
    [ProtoContract]
    internal record MessageRevokeAllowance(
        [property: ProtoMember(1, Name = "granter"), JsonPropertyName("granter")] string GranterAddress,
        [property: ProtoMember(2, Name = "grantee"), JsonPropertyName("grantee")] string GranteeAddress) : SerializerMessage(FeeGrant.MessageRevokeAllowance.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new FeeGrant.MessageRevokeAllowance(this.TypeUrl, this.GranterAddress, this.GranteeAddress);
        }
    }
}
