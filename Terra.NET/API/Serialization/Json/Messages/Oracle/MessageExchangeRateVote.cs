using Google.Protobuf;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.Oracle;

namespace Terra.NET.API.Serialization.Json.Messages.Oracle
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageExchangeRateVote(string Salt, [property: JsonPropertyName("feeder")] string FeederAddress, [property: JsonPropertyName("validator")] string ValidatorAddress, string ExchangeRates)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "oracle/MsgAggregateExchangeRateVote";
        public const string COSMOS_DESCRIPTOR = "/terra.oracle.v1beta1.MsgAggregateExchangeRateVote";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Oracle.MessageExchangeRateVote(this.Salt, this.FeederAddress, this.ValidatorAddress, DecodeExchangeRates(this.ExchangeRates));
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgAggregateExchangeRateVote
            {
                Salt = this.Salt,
                Feeder = this.FeederAddress,
                Validator = this.ValidatorAddress,
                ExchangeRates = this.ExchangeRates
            };
        }

        internal static string EncodeExchangeRates(DenomSwapRate[] exchangeRates)
        {
            StringBuilder sb = new();

            foreach (var rate in exchangeRates)
            {
                sb.Append($"{rate.SwapRate}{rate.Denom},");
            }

            var finalString = sb.ToString();
            return finalString[0..^2];
        }

        private static DenomSwapRate[] DecodeExchangeRates(string exchangeRates)
        {
            var splittedRates = exchangeRates.Split(',');
            var swapRates = new DenomSwapRate[splittedRates.Length];

            for (int ix = 0; ix < splittedRates.Length; ix++)
            {
                var rate = splittedRates[ix];
                swapRates[ix] = new DenomSwapRate(rate.Substring(rate.Length - 5, 4), decimal.Parse(rate[0..^6]));
            }

            return swapRates;
        }
    }
}
