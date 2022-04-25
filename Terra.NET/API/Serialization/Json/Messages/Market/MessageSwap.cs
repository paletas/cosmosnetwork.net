using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.Market;

namespace Terra.NET.API.Serialization.Json.Messages.Market
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageSwap([property: JsonPropertyName("trader")] string TraderAddress, string AskDenom, DenomAmount OfferCoin)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "market/MsgSwap";
        public const string COSMOS_DESCRIPTOR = "/cosmos.market.v1beta1.MsgSwap";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Market.MessageSwap(this.TraderAddress, this.AskDenom, this.OfferCoin.ToModel());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var swap = new MsgSwap
            {
                Trader = this.TraderAddress,
                AskDenom = this.AskDenom,
                OfferCoin = this.OfferCoin.ToProto()
            };

            return swap;
        }
    }
}
