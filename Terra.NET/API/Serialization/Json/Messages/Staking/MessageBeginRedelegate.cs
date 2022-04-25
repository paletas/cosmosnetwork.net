using Cosmos.SDK.Protos.Staking;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Staking
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageBeginRedelegate(string DelegatorAddress, [property: JsonPropertyName("validator_src_address")] string SourceValidatorAddress, [property: JsonPropertyName("validator_dst_address")] string DestinationValidatorAddress, DenomAmount Amount)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "staking/MsgBeginRedelegate";
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgBeginRedelegate";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Staking.MessageBeginRedelegate(this.DelegatorAddress, this.SourceValidatorAddress, this.DestinationValidatorAddress, this.Amount.ToModel());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgBeginRedelegate
            {
                DelegatorAddress = this.DelegatorAddress,
                ValidatorSrcAddress = this.SourceValidatorAddress,
                ValidatorDstAddress = this.DestinationValidatorAddress,
                Amount = this.Amount.ToProto()
            };
        }
    }
}
