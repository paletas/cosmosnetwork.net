using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Authz.Serialization
{
    [ProtoContract]
    internal record MessageRevoke(
        [property: ProtoMember(1, Name = "granter"), JsonPropertyName("granter")] string GranterAddress,
        [property: ProtoMember(2, Name = "grantee"), JsonPropertyName("grantee")] string GranteeAddress,
        string MessageTypeUrl)
        : SerializerMessage(Authz.MessageRevoke.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new CosmosNetwork.Modules.Authz.MessageRevoke(this.GranterAddress, this.GranteeAddress, this.MessageTypeUrl);
        }
    }
}
