using Google.Protobuf;
using System.Text.Json;
using TerraMoney.SDK.Core.Protos.Market;

namespace Terra.NET.API.Serialization.Json.Messages.Market
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageSwapSend(string FromAddress, string ToAddress, string AskDenom, DenomAmount OfferCoin)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "market/MsgSwapSend";
        public const string COSMOS_DESCRIPTOR = "/terra.market.v1beta1.MsgSwapSend";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Market.MessageSwapSend(
                this.FromAddress,
                this.ToAddress,
                this.AskDenom,
                this.OfferCoin.ToModel());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgSwapSend
            {
                FromAddress = this.FromAddress,
                ToAddress = this.ToAddress,
                AskDenom = this.AskDenom,
                OfferCoin = this.OfferCoin.ToProto()
            };
        }
    }
}
