using Cosmos.SDK.Protos.Distribution;
using Google.Protobuf;
using System.Text.Json;

namespace Terra.NET.API.Serialization.Json.Messages.Distribution
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageSetWithdrawAddress(string DelegatorAddress, string WithdrawAddress)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "distribution/MsgSetWithdrawAddress";
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgSetWithdrawAddress";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Distribution.MessageSetWithdrawAddress(this.DelegatorAddress, this.WithdrawAddress);
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgSetWithdrawAddress
            {
                DelegatorAddress = this.DelegatorAddress,
                WithdrawAddress = this.WithdrawAddress
            };
        }
    }
}
