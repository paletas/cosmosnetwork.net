using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Authz.Serialization
{
    [ProtoContract]
    internal record MessageGrant(
        [property: ProtoMember(1, Name = "granter"), JsonPropertyName("granter")] string GranterAddress,
        [property: ProtoMember(2, Name = "grantee"), JsonPropertyName("grantee")] string GranteeAddress,
        [property: ProtoMember(3, Name = "grant")] Grant Grant) : SerializerMessage(Authz.MessageGrant.COSMOS_DESCRIPTOR)
    {
        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Modules.Authz.MessageGrant(
                this.GranterAddress,
                this.GranteeAddress,
                this.Grant.ToModel());
        }
    }
}
