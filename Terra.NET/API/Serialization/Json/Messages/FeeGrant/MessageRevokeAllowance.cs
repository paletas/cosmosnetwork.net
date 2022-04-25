using Cosmos.SDK.Protos.FeeGrant;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.FeeGrant
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageRevokeAllowance([property: JsonPropertyName("granter")] string GranterAddress, [property: JsonPropertyName("grantee")] string GranteeAddress)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "feegrant/MsgRevokeAllowance";
        public const string COSMOS_DESCRIPTOR = "/cosmos.feegrant.v1beta1.MsgRevokeAllowance";

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgRevokeAllowance
            {
                Granter = this.GranterAddress,
                Grantee = this.GranteeAddress
            };
        }

        internal override NET.Message ToModel()
        {
            return new NET.Messages.FeeGrant.MessageRevokeAllowance(this.GranterAddress, this.GranteeAddress);
        }
    }
}
