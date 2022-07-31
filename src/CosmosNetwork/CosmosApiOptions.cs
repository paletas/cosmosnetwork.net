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
            JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };

            JsonSerializerOptions.Converters.Add(new BigIntegerConverter());
            JsonSerializerOptions.Converters.Add(new SignerModeConverter());
            JsonSerializerOptions.Converters.Add(new BlockFlagConverter());
            JsonSerializerOptions.Converters.Add(new SignerModeConverter());
        }

        public CosmosApiOptions(int? throttlingEnumeratorsInSeconds = default, ulong? startingBlockHeightForTransactionSearch = default)
            : this()
        {
            if (throttlingEnumeratorsInSeconds != default)
                ThrottlingEnumeratorsInMilliseconds = throttlingEnumeratorsInSeconds;

            if (startingBlockHeightForTransactionSearch.HasValue)
                StartingBlockHeightForTransactionSearch = startingBlockHeightForTransactionSearch.Value;
        }

        public string ChainId { get; set; } = "columbus-5";

        public int? ThrottlingEnumeratorsInMilliseconds { get; set; } = 1000;

        public ulong StartingBlockHeightForTransactionSearch { get; set; } = 1;

        public CoinDecimal[]? GasPrices { get; set; } = null;

        public decimal GasAdjustment { get; set; } = 1.75M;

        public string[]? DefaultDenoms { get; set; } = new[] { "uusd" };

        public JsonSerializerOptions JsonSerializerOptions { get; init; } = null!;
    }
}
