using Cosmos.SDK.Protos.Slashing;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Slashing
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageUnjail([property: JsonPropertyName("validator_addr")] string ValidatorAddress)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "slashing/MsgUnjail";
        public const string COSMOS_DESCRIPTOR = "/cosmos.slashing.v1beta1.MsgUnjail";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Slashing.MessageUnjail(this.ValidatorAddress);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgUnjail
            {
                ValidatorAddr = this.ValidatorAddress
            };
        }
    }
}

