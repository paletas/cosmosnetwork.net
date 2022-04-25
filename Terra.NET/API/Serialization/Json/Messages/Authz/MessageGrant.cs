using Cosmos.SDK.Protos.Authz;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Authz
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageGrant([property: JsonPropertyName("granter")] string GranterAddress, [property: JsonPropertyName("grantee")] string GranteeAddress)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "msgauth/MsgGrant";
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgGrant";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Authz.MessageGrant(
                this.GranterAddress,
                this.GranteeAddress);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgGrant
            {
                Granter = this.GranterAddress,
                Grantee = this.GranteeAddress
            };
        }
    }
}
