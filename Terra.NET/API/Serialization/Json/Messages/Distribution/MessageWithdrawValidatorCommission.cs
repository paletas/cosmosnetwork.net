using Cosmos.SDK.Protos.Distribution;
using Google.Protobuf;
using System.Text.Json;

namespace Terra.NET.API.Serialization.Json.Messages.Distribution
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageWithdrawValidatorCommission(string ValidatorAddress)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "distribution/MsgWithdrawValidatorCommission";
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgWithdrawValidatorCommission";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Distribution.MessageWithdrawValidatorCommission(this.ValidatorAddress);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgWithdrawValidatorCommission
            {
                ValidatorAddress = this.ValidatorAddress
            };
        }
    }
}
