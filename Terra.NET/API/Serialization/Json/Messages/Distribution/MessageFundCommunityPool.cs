using Cosmos.SDK.Protos.Distribution;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Distribution
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageFundCommunityPool([property: JsonPropertyName("depositor")] string DepositorAddress, DenomAmount[] Amount)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "distribution/MsgFundCommunityPool";
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgFundCommunityPool";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Distribution.MessageFundCommunityPool(this.DepositorAddress, this.Amount.Select(amt => amt.ToModel()).ToArray());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var msg = new MsgFundCommunityPool
            {
                Depositor = this.DepositorAddress
            };
            msg.Amount.AddRange(this.Amount.Select(amt => amt.ToProto()));
            return msg;
        }
    }
}
