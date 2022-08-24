using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.Oracle;

namespace Terra.NET.API.Serialization.Json.Messages.Oracle
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageExchangeRatePrevote(string Hash, [property: JsonPropertyName("feeder")] string FeederAddress, [property: JsonPropertyName("validator")] string ValidatorAddress)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "oracle/MsgAggregateExchangeRatePrevote";
        public const string COSMOS_DESCRIPTOR = "/terra.oracle.v1beta1.MsgAggregateExchangeRatePrevote";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Oracle.MessageExchangeRatePrevote(this.Hash, this.FeederAddress, this.ValidatorAddress);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgAggregateExchangeRatePrevote
            {
                Hash = this.Hash,
                Feeder = this.FeederAddress,
                Validator = this.ValidatorAddress
            };
        }
    }
}
