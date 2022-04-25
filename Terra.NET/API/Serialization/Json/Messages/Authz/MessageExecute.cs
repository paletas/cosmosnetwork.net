using Cosmos.SDK.Protos.Authz;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Authz
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageExecute([property: JsonPropertyName("grantee")] string GranteeAddress, [property: JsonPropertyName("msgs")] Message[] Messages)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "msgauth/MsgExecAuthorized";
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgExecAuthorized";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Authz.MessageExecute(this.GranteeAddress, this.Messages.Select(msg => msg.ToModel()).ToArray());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var exec = new MsgExec
            {
                Grantee = this.GranteeAddress
            };
            exec.Msgs.AddRange(this.Messages.Select(msg => msg.PackAny()));
            return exec;
        }
    }
}
