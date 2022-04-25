using Cosmos.SDK.Protos.Authz;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Authz
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageRevoke([property: JsonPropertyName("granter")] string GranterAddress, [property: JsonPropertyName("grantee")] string GranteeAddress, string MessageTypeUrl)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "msgauth/MsgRevoke";
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgRevoke";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Authz.MessageRevoke(this.GranterAddress, this.GranteeAddress, this.MessageTypeUrl);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgRevoke
            {
                Granter = this.GranterAddress,
                Grantee = this.GranteeAddress,
                MsgTypeUrl = this.MessageTypeUrl
            };
        }
    }
}
