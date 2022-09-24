using CosmosNetwork.Serialization.Json;
using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork
{
    public class CosmosApiOptions
    {
        public CosmosApiOptions()
        {
        }

        public CosmosApiOptions(int? throttlingEnumeratorsInSeconds = default, ulong? startingBlockHeightForTransactionSearch = default)
        {
            if (throttlingEnumeratorsInSeconds != default)
            {
                ThrottlingEnumeratorsInMilliseconds = throttlingEnumeratorsInSeconds;
            }

            if (startingBlockHeightForTransactionSearch.HasValue)
            {
                StartingBlockHeightForTransactionSearch = startingBlockHeightForTransactionSearch.Value;
            }
        }

        internal CosmosMessageRegistry? MessageRegistry { get; set; }

        public string? ChainId { get; set; }

        public int? ThrottlingEnumeratorsInMilliseconds { get; set; } = 1000;

        public ulong StartingBlockHeightForTransactionSearch { get; set; } = 1;

        public CoinDecimal[]? GasPrices { get; set; } = null;

        public decimal GasAdjustment { get; set; } = 1.75M;

        public string[]? DefaultDenoms { get; set; } = new[] { "uatom" };

        private JsonSerializerOptions? _jsonSerializerOptions;
        public JsonSerializerOptions JsonSerializerOptions
        {
            get { return _jsonSerializerOptions ?? (_jsonSerializerOptions = CreateSerializerOptions()); }
        }

        private JsonSerializerOptions CreateSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };

            options.Converters.Add(new Uint128Converter());
            options.Converters.Add(new Int128Converter());
            options.Converters.Add(new BlockFlagConverter());
            options.Converters.Add(new DurationConverter());
            options.Converters.Add(new TimeSpanConverter());
            options.Converters.Add(new TimestampConverter());
            options.Converters.Add(new MessagesConverter(this.MessageRegistry ?? throw new InvalidOperationException()));

            return options;
        }
    }
}
