using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.Oracle;

namespace Terra.NET.API.Serialization.Json.Messages.Oracle
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageDelegateFeedConsent([property: JsonPropertyName("operator")] string OperatorAddress, [property: JsonPropertyName("delegate")] string DelegateAddress)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "oracle/MsgDelegateFeedConsent";
        public const string COSMOS_DESCRIPTOR = "/terra.oracle.v1beta1.MsgDelegateFeedConsent";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Oracle.MessageDelegateFeedConsent(this.OperatorAddress, this.DelegateAddress);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgDelegateFeedConsent
            {
                Operator = this.OperatorAddress,
                Delegate = this.DelegateAddress
            };
        }
    }
}
